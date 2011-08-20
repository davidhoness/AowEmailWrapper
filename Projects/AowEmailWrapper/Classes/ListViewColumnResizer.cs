using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Helpers;

namespace AowEmailWrapper.Classes
{
    public enum ColumnHeaderResizeStyle
    {
        None,
        ColumnContent,
        HeaderSize,
        ContentHeaderMax,
        Fill
    }

    public static class ListViewColumnResizer
    {
        public static void ResizeColumns(ListView theListView)
        {
            ColumnHeader fillColumn = null;
            int totalColumnWidth = 0;

            foreach (ColumnHeader column in theListView.Columns)
            {
                if (column.Tag != null)
                {
                    ColumnHeaderResizeStyle theStyle = ConfigHelper.ParseEnumString<ColumnHeaderResizeStyle>(column.Tag.ToString());

                    switch (theStyle)
                    {
                        case ColumnHeaderResizeStyle.ColumnContent:
                            column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                            totalColumnWidth += column.Width;
                            break;
                        case ColumnHeaderResizeStyle.HeaderSize:
                            column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                            totalColumnWidth += column.Width;
                            break;
                        case ColumnHeaderResizeStyle.ContentHeaderMax:
                            column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                            int headerSize = column.Width;
                            column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                            int columnContentSize = column.Width;

                            column.Width = (headerSize > columnContentSize) ? headerSize : columnContentSize;
                            totalColumnWidth += column.Width;
                            break;
                        case ColumnHeaderResizeStyle.Fill:
                            fillColumn = column;
                            break;
                        default:
                            totalColumnWidth += column.Width;
                            break;
                    }
                }
            }

            if (fillColumn != null)
            {
                fillColumn.Width = theListView.Width - (totalColumnWidth + SystemInformation.VerticalScrollBarWidth);
                fillColumn.Width -= 4;
            }
        }
    }
}
