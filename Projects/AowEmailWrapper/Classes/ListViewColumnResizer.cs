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
        Fixed,
        Fill
    }

    public static class ListViewColumnResizer
    {
        private const char SplitChar = ';';

        public static void ResizeColumns(ListView theListView)
        {
            if (theListView.Columns.Count > 0 &&
                theListView.Items.Count > 0)
            {
                ColumnHeader fillColumn = null;
                int totalColumnWidth = 0;

                foreach (ColumnHeader column in theListView.Columns)
                {
                    if (column.Tag != null)
                    {
                        string style = column.Tag.ToString();
                        string value = string.Empty;

                        if (style.Contains(SplitChar))
                        {
                            string[] split = style.Split(SplitChar);
                            style = split[0];
                            value = split[1];
                        }

                        ColumnHeaderResizeStyle theStyle = ConfigHelper.ParseEnumString<ColumnHeaderResizeStyle>(style);

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
                            case ColumnHeaderResizeStyle.Fixed:
                                int width = 0;
                                if (int.TryParse(value, out width))
                                {
                                    column.Width = width;
                                }
                                totalColumnWidth += column.Width;
                                break;
                            default:
                                totalColumnWidth += column.Width;
                                break;
                        }
                    }
                }

                if (fillColumn != null)
                {
                    fillColumn.Width = theListView.ClientSize.Width - totalColumnWidth;
                }
            }
        }
    }
}
