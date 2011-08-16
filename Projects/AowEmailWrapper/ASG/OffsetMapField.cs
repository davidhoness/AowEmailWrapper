using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AowEmailWrapper.ASG
{
	public class OffsetMapField
	{
		public OffsetMapField ()
		{
			//StreamDataIsValid = false;
			Offset = -1;
			Length = -1;
			//Recognized = false;
		}

		public long Offset { get; set; }
		public long Length { get; set; }
		//public bool StreamDataIsValid { get; set; }
		//public BinaryAccessor BinaryAccessor { get; set; }
		//public FieldAccessor FieldAccessor { get; set; }
		//public bool Recognized { get; set; }

		public override string ToString ()
		{
			return String.Format( "{0} - {1} ({2} bytes)", Offset.ToString( "x" ), (Offset + Length).ToString("x"), Length );
		}
	}
}
