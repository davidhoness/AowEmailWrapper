using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AowEmailWrapper.Helpers;
using AowEmailWrapper.Games;
using Lesnikowski.Mail;

namespace AowEmailWrapper.ASG
{
    public enum ASGFileType : int
    {
        Unknown = 0,
        Aow1,
        Aow2Sm,
        AowMpe
    }

    public class ASGFileInfo : IDisposable
    {
        #region Private Members

        private ASGFileType _fileType = ASGFileType.Unknown;
        private AowGameType _gameType = AowGameType.Unknown;
        private MimeData _theAttachment;

        private string _gameTitle = null;
        private string _mapTitle = null;
        private int _turnNumber = 0;
        private int _modId = 0;
        private bool _isValid = true;

        private bool _fetch_compressed_data = true;

        private const string ASG_REGEX = ".[Aa][Ss][Gg]";
        private const string FileNameTrueTemplate = "{0}.asg";

        #endregion

        #region Public Properties

        //Inherent Properties

        public string FileName
        {
            get { return _theAttachment.FileName; }
        }

        public byte[] DataBytes
        {
            get { return _theAttachment.Data; }
        }

        public int Length
        {
            get { return _theAttachment.Data.Length; }
        }

        //Calculated Properties

        public ASGFileType FileType
        {
            get { return _fileType; }
        }

        public AowGameType GameType
        {
            get { return _gameType; }
        }

        public string GameTitle
        {
            get { return _gameTitle; }
        }

        public int TurnNumber
        {
            get { return _turnNumber; }
        }

        public int ModID
        {
            get { return _modId; }
        }

        public string MapTitle
        {
            get { return _mapTitle; }
        }

        public bool IsValid
        {
            get { return _isValid; }
        }

        public string FileNameTrue
        {
            get
            {
                string returnVal = null;

                if (!string.IsNullOrEmpty(_gameTitle))
                {
                    returnVal = string.Format(FileNameTrueTemplate, StringHelper.RemoveInvalidPathChars(_gameTitle));
                }

                if (string.IsNullOrEmpty(returnVal))
                {
                    returnVal = _theAttachment.FileName;
                }

                return returnVal;
            }
        }

        #endregion

        #region Constructors

        public ASGFileInfo(MimeData theAttachment)
        {
            _theAttachment = theAttachment;
            ParseProperties();
        }

        #endregion

        #region Private Methods

        private void ParseProperties()
        {
            try
            {
                ParseAowMajorVersion();

                switch (_fileType)
                {
                    case ASGFileType.Aow1:
                        ParseAow1();
                        break;
                    case ASGFileType.Aow2Sm:
                        ParseAow2(aowmap_signature);
                        break;
                    case ASGFileType.AowMpe:
                        ParseAow2(mpe_signature);
                        break;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Trace.Flush();

                _fileType = ASGFileType.Unknown;
                _gameType = AowGameType.Unknown;
            }
        }

        private void ParseAowMajorVersion()
        {
            switch (_theAttachment.Data[0])
            {
                case 0x43:
                    _fileType = ASGFileType.Aow1;
                    break;
                case 0x18:
                    switch (_theAttachment.Data[6])
                    {
                        case 0x58:
                            _fileType = ASGFileType.AowMpe;
                            break;
                        case 0x00:
                            _fileType = ASGFileType.Aow2Sm;
                            break;
                        default:
                            _fileType = ASGFileType.Unknown;
                            break;
                    }
                    break;
                default:
                    _fileType = ASGFileType.Unknown;
                    break;
            }
        }

        private void ParseAow1()
        {
            _gameType = AowGameType.Aow1;

            using (BinaryReader input = new BinaryReader(_theAttachment.GetMemoryStream()))
            {
                if (_fetch_compressed_data)
                {
                    if (CheckSignature(input, compressed_part_signature))
                    {
                        DataCompressor compressed_data = new DataCompressor(input, true);
                        compressed_data.Compressed = false;	//	decompression happens here

                        using (BinaryReader main_input = new BinaryReader(new MemoryStream(compressed_data.Data)))
                        {
                            if (CheckSignature(main_input, aow1map_signature))
                            {
                                int possible_class_id = main_input.ReadInt32();
                                main_input.BaseStream.Position += 3;	//	skip field list of root offsetmap, moving straight to the contents

                                OffsetMap map_om = new OffsetMap(main_input.BaseStream);
                                _mapTitle = map_om.ReadShortPascalString(0x19);
                                _turnNumber = map_om.ReadInt32(0x13);

                                OffsetMap section_15 = map_om.GetSubFieldOffsetMap(0x15);
                                OffsetMap s_15_1 = section_15.GetSubFieldOffsetMap(0x01);
                                OffsetMap s_15_1_1 = s_15_1.GetSubFieldOffsetMap(0x01, true);
                                OffsetMap s_15_1_1_36 = s_15_1_1.GetSubFieldOffsetMap(0x36);
                                OffsetMap s_15_1_1_36_15 = s_15_1_1_36.GetSubFieldOffsetMap(0x15);
                                _gameTitle = s_15_1_1_36_15.ReadShortPascalString(0x14);
                            }
                            else
                            {
                                _isValid = false;
                            }
                        }
                    }
                    else
                    {
                        _isValid = false;
                    }
                }
            }
        }

        private void ParseAow2(byte[] signature)
        {
            using (BinaryReader input = new BinaryReader(_theAttachment.GetMemoryStream()))
            {
                if (CheckSignature(input, signature))
                {
                    int header_length = input.ReadInt32() - 7;	//	хз почему, но это так
                    _modId = input.ReadInt32();

                    if (CheckSignature(input, magic_11_bytes))
                    {
                        long compressed_part_start = input.BaseStream.Position + header_length;
                        OffsetMap header_om = new OffsetMap(input.BaseStream, input.BaseStream.Position, header_length);

                        _mapTitle = header_om.ReadShortPascalString(0x20);
                        _turnNumber = header_om.ReadInt32(0x21);
                        byte playersCount = header_om.ReadByte(0x1e);

                        //	compressed info
                        if (_fetch_compressed_data)
                        {
                            input.BaseStream.Position = compressed_part_start;
                            if (CheckSignature(input, compressed_part_signature))
                            {
                                DataCompressor compressed_data = new DataCompressor(input, true);
                                compressed_data.Compressed = false;	//	decompression happens here

                                using (BinaryReader main_input = new BinaryReader(new MemoryStream(compressed_data.Data)))
                                {
                                    //	digging up the game title
                                    if (CheckSignature(main_input, decompressed_data_signature))
                                    {
                                        OffsetMap map_om = new OffsetMap(main_input.BaseStream);
                                        OffsetMap player_list_wrapper_om = map_om.GetSubFieldOffsetMap(0x1a);
                                        OffsetMap player_list_om = player_list_wrapper_om.GetSubFieldOffsetMap(0x01);
                                        OffsetMap player_1_om = player_list_om.GetSubFieldOffsetMap(0x01, true);
                                        OffsetMap player_1_pbem_settings_wrapper = player_1_om.GetSubFieldOffsetMap(0x36);
                                        OffsetMap player_1_pbem_settings = player_1_pbem_settings_wrapper.GetSubFieldOffsetMap(0x15);
                                        _gameTitle = player_1_pbem_settings.ReadShortPascalString(0x14);

                                        //	WT or SM?
                                        OffsetMap race_list_wrapper_om = map_om.GetSubFieldOffsetMap(0x1b);
                                        OffsetMap race_list_om = race_list_wrapper_om.GetSubFieldOffsetMap(0x01);
                                        int race_count = race_list_om.Fields.Count;

                                        switch (race_count)
                                        {
                                            case 15:
                                                _gameType = _fileType == ASGFileType.AowMpe ? AowGameType.AowMpe : AowGameType.AowSm;
                                                break;
                                            case 12:
                                                _gameType = AowGameType.Aow2;
                                                break;
                                            default:
                                                _gameType = AowGameType.Unknown;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        _isValid = false;
                                    }
                                }
                            }
                            else
                            {
                                _isValid = false;
                            }
                        }
                    }
                }
            }
        }

        private static bool CheckSignature(BinaryReader in_stream, byte[] signature)
        {
            byte[] actual = in_stream.ReadBytes(signature.Length);
            return actual.SequenceEqual(signature);
        }

        #endregion

        #region Public Methods

        public static bool IsAsg(string path)
        {
            return Regex.IsMatch(path, ASG_REGEX);
        }

        //==== Game Bug Compensation ====
        //The games have a bug in the save dialogue screen where an extra '.' in the file name will cause it to be truncated.
        //Example: "Midwinter (Dave, Fred, Rob) Upatch 1.4.asg" > press Save
        //       = "Midwinter (Dave, Fred, Rob) Upatch 1.asg"
        //This can cause the file saved to disk and the file name recorded for the email to differ.
        //This search pattern will compensate though.
        //There is also a case of the above where a space can occur before the the . [solved by returnVal.Trim()]
        //Example: "Midwinter (Dave, Fred, Rob)<space>.asg"
        public static string SafeSearchFileName(string fileName)
        {
            string returnVal = string.Empty;

            string fileNameTrim = fileName.Trim();
            if (fileNameTrim.Length > 0)
            {
                int firstDotIndex = fileNameTrim.IndexOf('.');
                returnVal = fileNameTrim.Substring(0, firstDotIndex);
            }

            return returnVal.Trim();
        }

        public bool SaveToFolder(string folderPath)
        {
            bool success = false;

            if (Directory.Exists(folderPath))
            {
                _theAttachment.Save(Path.Combine(folderPath, FileNameTrue));
                success = true;
            }

            return success;
        }

        #endregion

        #region Magic numbers

        private static readonly byte[] aowmap_signature = { 0x18, 0x00, 0x00, 0x00, 0x48, 0x4d, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] mpe_signature = { 0x18, 0x00, 0x00, 0x00, 0x48, 0x4d, 0x58, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] aow1map_signature = { 0x10, 0x00, 0x00, 0x00, 0x48, 0x53, 0x4d, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] magic_11_bytes = { 0x01, 0x00, 0x10, 0x01, 0x01, 0x00, 0x00, 0xed, 0x01, 0x10, 0x01 };
        private static readonly byte[] magic_7_bytes_2 = { 0x01, 0x1e, 0x00, 0x01, 0x01, 0x00, 0x00 };
        private static readonly byte[] magic_20_bytes = { 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00 };
        private static readonly byte[] compressed_part_signature = { 0x43, 0x46, 0x53, 0x00, 0x02 };
        private static readonly byte[] decompressed_data_signature = { 0x01, 0x00, 0x00 };

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this._theAttachment = null;
        }

        #endregion
    }
}