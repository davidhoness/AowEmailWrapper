using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.ASG
{
	public class OffsetMap
	{
		#region Construction

		private OffsetMap ()
		{
			Fields = new Dictionary<int, OffsetMapField>();
		}

		public OffsetMap (Stream storage_stream)
			: this( storage_stream, storage_stream.Position, storage_stream.Length - storage_stream.Position )
		{
		}
		
		public OffsetMap (Stream storage_stream, long offset, long object_size)
			:this()
		{
			StorageStream = storage_stream;
			ReadFromStream( offset, object_size );
		}

		#endregion

		#region Public

		public Dictionary<int, OffsetMapField> Fields { get; private set; }
		public Stream StorageStream { get; private set; }

		public int ReadInt32 (int field_id)
		{
			if ( Fields.ContainsKey( field_id ) )
			{
				StorageStream.Position = Fields[field_id].Offset;
				BinaryReader input = new BinaryReader( StorageStream );
				return input.ReadInt32();
			}
			else
				return -1;
		}

		public byte ReadByte (int field_id)
		{
			if ( Fields.ContainsKey( field_id ) )
			{
				StorageStream.Position = Fields[field_id].Offset;
				BinaryReader input = new BinaryReader( StorageStream );
				return input.ReadByte();
			}
			else
				return 0;
		}

		public string ReadShortPascalString (int field_id)
		{
			if ( Fields.ContainsKey( field_id ) )
			{
				StorageStream.Position = Fields[field_id].Offset;
				BinaryReader input = new BinaryReader( StorageStream );
				byte string_length = input.ReadByte();
				byte[] raw_string = input.ReadBytes( string_length );
				return Encoding.Default.GetString( raw_string );
			}
			else
				return String.Empty;
		}

        public OffsetMap GetSubFieldOffsetMap(int field_id)
        {
            return GetSubFieldOffsetMap(field_id, false);
        }

		public OffsetMap GetSubFieldOffsetMap (int field_id, bool has_class_id)
		{
			if ( Fields.ContainsKey( field_id ) )
			{
				if ( !has_class_id )
					return new OffsetMap( StorageStream, Fields[field_id].Offset, Fields[field_id].Length );
				else
					return new OffsetMap( StorageStream, Fields[field_id].Offset + 4, Fields[field_id].Length - 4 );
			}
			else
				return null;
		}

		#endregion

		#region Private

		private IEnumerable<int> ReadFromStream (long offset, long length)
		{
			List<int> new_fields = new List<int>();

			StorageStream.Position = offset;
			BinaryReader input = new BinaryReader( StorageStream );	//	no 'using'!!! must NOT close the storage stream

				//	кол-во оффсетов
			byte short_off_count = input.ReadByte();
			int long_off_count = 0;

			if ( short_off_count >= 0x80 )	//	0x80 - признак наличия длинных оффсетов
			{
				short_off_count -= 0x80;
				long_off_count = input.ReadInt32();
			}

				//	относительные оффсеты в том виде, как они записаны в файле
			Queue<OffsetRecord> offsets = new Queue<OffsetRecord>();

			for ( int i=0; i < short_off_count; ++i )
			{
				OffsetRecord new_offset = new OffsetRecord
				{
					ID = input.ReadByte(),
					Offset = input.ReadByte()
				};
				offsets.Enqueue( new_offset );
			}

			for ( int i=0; i < long_off_count; ++i )
			{
				OffsetRecord new_offset = new OffsetRecord
				{
					ID = input.ReadInt32(),
					Offset = input.ReadInt32()
				};
				offsets.Enqueue( new_offset );
			}

			long data_absolute_offset = input.BaseStream.Position;		//	относительно начала потока
			long data_relative_offset = data_absolute_offset - offset;	//	относительно начала фрейма - для расчёта длины последнего поля

				//	при перенесении оффсетов в оффсетмап надо заменить их на абсолютные (в потоке) и добавить длины
			while ( offsets.Count > 0 )
			{
				OffsetRecord current = offsets.Dequeue();

				long absolute_offset = data_absolute_offset + current.Offset;
				long next_offset = ( offsets.Count > 0 ) ? offsets.Peek().Offset : length - data_relative_offset;
				long offset_length = next_offset - current.Offset;

				OffsetMapField field_record = GetOrCreateFieldRecord( current.ID );

				field_record.Offset = absolute_offset;
				field_record.Length = offset_length;
				//field_record.StreamDataIsValid = true;

				new_fields.Add( current.ID );
			}

			return new_fields;
		}

		private OffsetMapField GetOrCreateFieldRecord (int field_id)
		{
			if ( !Fields.ContainsKey( field_id ) )
				Fields.Add( field_id, new OffsetMapField() );

			return Fields[field_id];
		}

		private struct OffsetRecord
		{
			public int ID { get; set; }
			public long Offset { get; set; }
		}

		#endregion
	}
}
