using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AowEmailWrapper.Localization;

namespace AowEmailWrapper.ConfigFramework
{
    public enum EmailSaveFolder
    {
        [XmlEnum(Name = "EmailIn")]
        EmailIn,
        [XmlEnum(Name = "Save")]
        Save
    }

    [XmlRoot("preferences_config")]
    public class PreferencesConfigValues
    {
        private bool _playSoundOnEmail;
        private bool _playSoundOnSend;
        private bool _autostart;
        private EmailSaveFolder _saveFolder = EmailSaveFolder.EmailIn;
        private string _languageCode = Translator.DefaultLanguageCode;

        [XmlAttribute("playsoundonemail")]
        public bool PlaySoundOnEmail
        {
            get { return _playSoundOnEmail; }
            set { _playSoundOnEmail = value; }
        }

        [XmlAttribute("playsoundonsend")]
        public bool PlaySoundOnSend
        {
            get { return _playSoundOnSend; }
            set { _playSoundOnSend = value; }
        }

        [XmlAttribute("autostart")]
        public bool Autostart
        {
            get { return _autostart; }
            set { _autostart = value; }
        }

        [XmlAttribute("save_folder")]
        public EmailSaveFolder SaveFolder
        {
            get { return _saveFolder; }
            set { _saveFolder = value; }
        }

        [XmlAttribute("languageCode")]
        public string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; }
        }

        public PreferencesConfigValues()
            : this(false)
        { }

        public PreferencesConfigValues(bool defaults)
        {
            if (defaults)
            {
                _playSoundOnEmail = true;
                _playSoundOnSend = true;
                _autostart = false;
                _saveFolder = EmailSaveFolder.EmailIn;
            }
        }
    }
}
