//Copyright(C) -  2017  Adam Richard
//LPB Archive is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.If not, see<https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPB_Archive
{
    class Options
    {
        public static string var_write_path = @"e:\tape\";
        public static string var_read_path = @"e:\cap\";
        public static string var_tools_path = @"e:\Tools\bin\";
        public static string var_mi_archive_path = @"\\vault3\archive\MediaInfo_XML\";
        public static string var_md5_path = var_tools_path + "md5deep64.exe";
        public static string var_mediainfo_path = var_tools_path + "MediaInfo.exe";
        public static string var_status = "Not Running";


    }
}
