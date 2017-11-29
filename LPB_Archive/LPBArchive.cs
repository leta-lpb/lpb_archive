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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;




namespace LPB_Archive
{
    public partial class LPBArchive : Form
    {
    

        public LPBArchive()
        {
            InitializeComponent();
            System.Windows.Forms.MessageBox.Show("    LPB Archive is free software: you can redistribute it and or modify" + Environment.NewLine + 
                "    it under the terms of the GNU General Public License as published by" + Environment.NewLine + 
                "    the Free Software Foundation, either version 3 of the License, or " + Environment.NewLine + 
                "    (at your option) any later version." + Environment.NewLine + Environment.NewLine +
                "    This program is distributed in the hope that it will be useful," + Environment.NewLine + 
                "    but WITHOUT ANY WARRANTY; without even the implied warranty of" + Environment.NewLine + 
                "    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the" + Environment.NewLine + 
                "    GNU General Public License for more details." + Environment.NewLine + Environment.NewLine +
                "    You should have received a copy of the GNU General Public License" + Environment.NewLine +
                "    along with this program. If not, see https://www.gnu.org/licenses/");
            txt_output_path.Text = Properties.Settings.Default.var_write_path;
            txt_input_path.Text = Properties.Settings.Default.var_read_path;
            txt_tools_path.Text = Properties.Settings.Default.var_tools_path;
            lbl_status.Text = Properties.Settings.Default.var_status;
            checkBox1.Checked = Properties.Settings.Default.var_delete_box;

        }


        public void m_statuschange(string state)
        {

            if (state == "On")
            {
                Properties.Settings.Default.var_status = "Running";
                lbl_status.Text = Properties.Settings.Default.var_status;
                
            }
            if (state == "Off")
            {
                Properties.Settings.Default.var_status = "Not Running";
                lbl_status.Text = Properties.Settings.Default.var_status;
            }
        }



        public void filegrabber(string path)
        {

            //This is the function that does the heavy lifting of moving the file, hashing, QA, and generating the web file. This is called by the achive and verify button. 
            string[] dirs = Directory.GetFiles(path, "*.mov");

            foreach (string arch_file in dirs)
            {
                vars.glob.var_master_bool = false;
                vars.glob.failcheck = false;
                vars.glob.webfailcheck = false;
                vars.glob.mediainfo_web_copy_target = "";
                vars.glob.filenameext = Path.GetFileName(arch_file);
                vars.glob.filename = Path.GetFileNameWithoutExtension(arch_file);
                vars.glob.filedir = Path.GetDirectoryName(arch_file);
                vars.glob.final_path = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filenameext;
                vars.glob.final_web_path = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + "_mezzanine.mp4";
                vars.glob.var_mov_frame_md5 = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + "_mov_framemd5.txt";
                vars.glob.var_ffv1_frame_md5 = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + "_ffv1_framemd5.txt";
                //                vars.glob.var_mov_frame_md5_edit = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + "_mov_framemd5";
                //                vars.glob.var_ffv1_frame_md5_edit = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + "_ffv1_framemd5";
                vars.glob.var_del_check_bool = Properties.Settings.Default.var_delete_box;

                Console.WriteLine("The current file I'm working on is " + arch_file);
                Console.WriteLine("The current file output I'm working on is " + vars.glob.final_path);
                vars.glob.var_ffv1filename = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + "_ffv1.mkv";
                vars.glob.var_movfilename = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + vars.glob.filename + ".mov";
                m_create_folder(vars.glob.filename);
                m_move_file(arch_file, vars.glob.final_path);
                m_ffv1_encode(vars.glob.final_path, vars.glob.var_ffv1filename);
                m_ffv1_framehash();
                m_set_master();
                m_hash(vars.glob.var_master_file);
                m_mediainfo(vars.glob.var_master_file);
                m_mediaconch(vars.glob.var_master_file);



                m_web_encode(vars.glob.var_master_file, vars.glob.final_web_path);
                // We don't copy the website in anymore
//                m_web_copy(vars.glob.final_web_path);  
                // mez hash
                m_hash(vars.glob.final_web_path);
                // mez media info
                m_mediainfo(vars.glob.final_web_path);
                // mez media conch
                m_mediaconch(vars.glob.final_web_path);
                // Copy the mez file to the new location
//                m_copy_file(vars.glob.mediainfo_web_copy_source, vars.glob.mediainfo_web_copy_target);


            }

           
        }

        public static void m_mediaconch(string file)
        {
            String var_filename;
            String var_output;
            var_filename = Path.GetFileNameWithoutExtension(file);
            var_output = (Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + "_MediaConch.html");

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_mediaconch_path;
            if (var_filename.Contains("_mezzanine"))
            {
                start.Arguments = "\"" + file + "\"" + " -p " + Properties.Settings.Default.var_tools_path + "Mezzanine-policy.xml -fh";
            }
            else
            {
                if (var_filename.Contains("_ffv1"))
                {
                    start.Arguments = "\"" + file + "\"" + " -p " + Properties.Settings.Default.var_tools_path + "FFV1-policy.xml -fh";
                }
                else
                {
                    start.Arguments = "\"" + file + "\"" + " -p " + Properties.Settings.Default.var_tools_path + "mov-policy.xml -fh";
                }
            }
            
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    System.IO.File.WriteAllText(var_output, result);
                }
            }
            if (var_filename.Contains("_mezzanine"))
            {
                vars.glob.var_mez_conchcheck = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + "_MediaConch.html";
            }
            else
            {
                vars.glob.var_conchcheck = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + "_MediaConch.html";
            }

        }


        public static bool m_mediaconch_check(string name)
        {

            String var_output;
            

            var_output = (name + "_MediaConch.html");

            if (System.IO.File.Exists(var_output))
            {

                string readtext = File.ReadAllText(var_output);
                if (readtext.Contains("fail"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }





        public static void m_set_master()

        {
            //string line = null;
            //int line_number = 0;
            //int line_to_delete = 9;

            //using (StreamReader reader = new StreamReader(vars.glob.var_ffv1_frame_md5_edit))
            //{
            //    using (StreamWriter writer = new StreamWriter(vars.glob.var_ffv1_frame_md5))
            //    {
            //        while ((line = reader.ReadLine()) != null)
            //        {
            //            line_number++;

            //            if (line_number == line_to_delete)
            //                continue;

            //            writer.WriteLine(line);
            //        }
            //    }
            //}

            //line_number = 0;

            //using (StreamReader reader = new StreamReader(vars.glob.var_mov_frame_md5_edit))
            //{
            //    using (StreamWriter writer = new StreamWriter(vars.glob.var_mov_frame_md5))
            //    {
            //        while ((line = reader.ReadLine()) != null)
            //        {
            //            line_number++;

            //            if (line_number == line_to_delete)
            //                continue;

            //            writer.WriteLine(line);
            //        }
            //    }
            //}

            FileInfo movmd5 = new FileInfo(vars.glob.var_mov_frame_md5);
            FileInfo ffv1md5 = new FileInfo(vars.glob.var_ffv1_frame_md5);
            vars.glob.var_master_bool = m_framemd5_compare(movmd5, ffv1md5);
            if (vars.glob.var_master_bool == true)
            {
                Console.WriteLine("The MKV is right");
                vars.glob.var_master_file = vars.glob.var_ffv1filename;
                if (vars.glob.var_del_check_bool == true)
                {
                    m_del_file(vars.glob.var_movfilename);
                }

                
                
                

            }
            else
            {
                Console.WriteLine("The mov is right");
                vars.glob.var_master_file = vars.glob.var_movfilename;
                if (vars.glob.var_del_check_bool == true)
                {
                    m_del_file(vars.glob.var_movfilename);
                }
            }




        }





        public static void m_hash(string file)
        {
            String var_filename;
            String var_output;
            var_filename = Path.GetFileNameWithoutExtension(file);
            var_output = (Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + ".md5.txt");
            if (System.IO.File.Exists(file))
            {

                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(file))
                    {
                        var str = BitConverter.ToString(md5.ComputeHash(stream)).Replace("??", "‌​").Replace("-", "").ToLower();
                        System.IO.File.WriteAllText(var_output, str);
                    }
                }
            }
            else
            {
                
            }

            if (var_filename.Contains("_mezzanine"))
            {
                vars.glob.var_mez_hash = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + ".md5.txt";
            }
            else
            {
                vars.glob.var_master_hash = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + ".md5.txt";
            }

        }





        public static void m_mediainfo(string file)
        {
            String var_filename;
            String var_output;
            var_filename = Path.GetFileNameWithoutExtension(file);
            var_output = (Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + "_Mediainfo.xml");

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_mediainfo_path;
            start.Arguments = "--output=PBCore2 " + "\"" + file + "\"";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    System.IO.File.WriteAllText(var_output, result);
                }
            }
            if (var_filename.Contains("_mezzanine"))
            {
                vars.glob.mediainfo_web_copy_source = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + "_Mediainfo.xml";

            }
            else
            {
                vars.glob.mediainfo_copy_source = Properties.Settings.Default.var_write_path + vars.glob.filename + @"\" + var_filename + "_Mediainfo.xml";

            }
        }

        public static void m_ffv1_encode(string fullpath, string ffv1path)
        {
            if (System.IO.File.Exists(vars.glob.var_ffv1filename))
            {
                Console.WriteLine("The file " + vars.glob.var_ffv1filename + " already exists");
            }
            else
            {
                Process start = new Process();
                start.StartInfo.FileName = Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_ffmpeg_path;
                start.StartInfo.Arguments = "-i " + "\"" + fullpath + "\"" + " -c:v ffv1 -level 3 -g 1 -c:a copy " + "\"" + vars.glob.var_ffv1filename + "\"" + " -f framemd5 -an " + "\"" + vars.glob.var_mov_frame_md5 + "\"";
                start.StartInfo.UseShellExecute = true;
                Console.WriteLine("The arguements used for the " + System.Reflection.MethodInfo.GetCurrentMethod().Name + " method are: " + start.StartInfo.Arguments);
                //start.StartInfo.RedirectStandardOutput = true;
                start.Start();
                start.WaitForExit();
            }
        }


        public static void m_ffv1_framehash()
        {
            if (System.IO.File.Exists(vars.glob.var_ffv1_frame_md5))
            {
                Console.WriteLine("The file " + vars.glob.var_ffv1_frame_md5 + " already exists");
            }
            else
            {
                Process start = new Process();
                start.StartInfo.FileName = Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_ffmpeg_path;
                start.StartInfo.Arguments = "-i " + "\"" + vars.glob.var_ffv1filename + "\"" + "  -f framemd5  -an " + "\"" +vars.glob.var_ffv1_frame_md5 + "\"";
                start.StartInfo.UseShellExecute = true;
                Console.WriteLine("The arguements used for the " + System.Reflection.MethodInfo.GetCurrentMethod().Name + " method are: " + start.StartInfo.Arguments);
                //start.StartInfo.RedirectStandardOutput = true;
                start.Start();
                start.WaitForExit();
            }
        }


        public static void m_web_encode(string ffv1, string webpath)
        {
            if (System.IO.File.Exists(webpath))
            {
                Console.WriteLine("The file " + webpath + " already exists");
            }
            else
            {
                Process start = new Process();
                start.StartInfo.FileName = Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_ffmpeg_path;
                start.StartInfo.Arguments = "-i " + "\"" + ffv1 + "\"" + " -c:v libx264 -c:a aac -b:a 320k -b:v 5000k -vf \"yadif =0:-1:0\" " + "\"" + webpath + "\"";
                start.StartInfo.UseShellExecute = true;
                Console.WriteLine("The arguements used for the " + System.Reflection.MethodInfo.GetCurrentMethod().Name + " method are: " + start.StartInfo.Arguments);
                //start.StartInfo.RedirectStandardOutput = true;
                start.Start();
                start.WaitForExit();
            }
        }


        const int BYTES_TO_READ = sizeof(Int64);

        static bool m_framemd5_compare(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            if (first.FullName == second.FullName)
                return true;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }






        public static void m_create_folder(string args)
        {
            String var_filename;
            var_filename = Path.GetFileNameWithoutExtension(args);
            Console.WriteLine("The filename variable is " + var_filename);
            if (System.IO.Directory.Exists(Properties.Settings.Default.var_write_path + var_filename))
            {
                Console.WriteLine("The folder " + Properties.Settings.Default.var_write_path + var_filename + " already exists");
            }
            else
            {
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.var_write_path + var_filename);
                Console.WriteLine("Created " + Properties.Settings.Default.var_write_path + var_filename);
            }
        }


        public static void m_move_file(string args, string fp)
        {
        
            System.IO.File.Move(args, fp);
        }

        public static void m_copy_file(string args, string fp)
        {
            if (System.IO.File.Exists(fp))
            {
            }
            else
            {
            System.IO.File.Copy(args, fp);
            }
        }

        public static void m_del_file(string nnfile)
        {
            System.IO.File.Delete(nnfile);
        }

        public static void m_web_copy(string name)
        {
            string tempname;
            tempname = Path.GetFileNameWithoutExtension(name);
            if (System.IO.File.Exists((name)))
            {
                m_copy_file(name, Properties.Settings.Default.var_web_path + @"\" + tempname + ".mp4");
            }

        }

        private void txt_output_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.var_write_path = txt_output_path.Text;
        }

        private void txt_input_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.var_read_path = txt_input_path.Text;
        }

        private void txt_tools_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.var_tools_path = txt_tools_path.Text;
        }

        private void btn_verify_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_ffmpeg_path) && System.IO.File.Exists(Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_mediainfo_path) && System.IO.File.Exists(Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_mediaconch_path))
            {

                m_statuschange("On");
                String readpath = Properties.Settings.Default.var_read_path;
                this.Refresh();
                filegrabber(readpath);

                vars.glob.details = "";
                listView1.Clear();
                listView1.Columns.Clear();
                listView1.View = View.Details;
                listView1.Columns.Add("Filename", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("File Check", 70, HorizontalAlignment.Left);
                listView1.Columns.Add("Details", 450, HorizontalAlignment.Left);

                DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default.var_write_path);
                foreach (var fi in di.GetDirectories())
                {
                    vars.glob.var_master_bool = false;
                    vars.glob.failcheck = false;
                    vars.glob.webfailcheck = false;
                    vars.glob.mediainfo_web_copy_target = "";
                    vars.glob.mediainfo_web_copy_source = "";
                    vars.glob.details = "";
                    vars.glob.var_dirfilename = fi.Name;
                    vars.glob.filename = fi.Name;


                    FileInfo movmd5 = new FileInfo(Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_mov_framemd5.txt");
                    FileInfo ffv1md5 = new FileInfo(Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_ffv1_framemd5.txt");
                    vars.glob.var_master_bool = m_framemd5_compare(movmd5, ffv1md5);
                    if (vars.glob.var_master_bool == true)
                    {
                        Console.WriteLine(Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_ffv1.mkv" + " is the correct file");
                        vars.glob.var_master_file = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_ffv1.mkv";
                        vars.glob.var_master_file_noext = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_ffv1";
                        vars.glob.var_ffv1filename = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_ffv1.mkv";
                    }
                    else
                    {
                        Console.WriteLine(Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + ".mov" + " is the correct file");
                        vars.glob.var_master_file = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + ".mov";
                        vars.glob.var_master_file_noext = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename;
                        vars.glob.var_movfilename = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + ".mov";
                    }



                    vars.glob.final_web_path = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_mezzanine.mp4";
                    vars.glob.var_web_file_noext = Properties.Settings.Default.var_write_path + vars.glob.var_dirfilename + @"\" + vars.glob.var_dirfilename + "_mezzanine";

                    vars.glob.var_movcheck = (vars.glob.var_master_file);
                    vars.glob.var_mediainfocheck = (vars.glob.var_master_file_noext + "_Mediainfo.xml");
                    vars.glob.var_mediaconchcheck = (vars.glob.var_master_file_noext + "_MediaConch.html");
                    vars.glob.var_hashcheck = (vars.glob.var_master_file_noext + ".md5.txt");
                    vars.glob.var_webcheck = (vars.glob.final_web_path);
                    vars.glob.var_webshare = (Properties.Settings.Default.var_web_path + @"\" + vars.glob.var_dirfilename + "_mezzanine.mp4");
                    vars.glob.var_web_mediainfocheck = (vars.glob.var_web_file_noext + "_Mediainfo.xml");
                    vars.glob.var_web_mediaconchcheck = (vars.glob.var_web_file_noext + "_MediaConch.html");
                    vars.glob.var_web_hashcheck = (vars.glob.var_web_file_noext + ".md5.txt");
                    ListViewItem item = new ListViewItem(fi.Name);
                    this.listView1.Items.Add(item);
                    vars.glob.failcheck = (m_mediaconch_check(vars.glob.var_master_file_noext));
                    vars.glob.webfailcheck = (m_mediaconch_check(vars.glob.var_web_file_noext));
                    if ((System.IO.File.Exists(vars.glob.var_mediainfocheck)) && (System.IO.File.Exists(vars.glob.var_hashcheck)) && (System.IO.File.Exists(vars.glob.var_movcheck)) && (System.IO.File.Exists(vars.glob.var_mediaconchcheck)) && (System.IO.File.Exists(vars.glob.var_web_mediainfocheck)) && (System.IO.File.Exists(vars.glob.var_web_mediaconchcheck)) && (System.IO.File.Exists(vars.glob.var_web_hashcheck)) && (vars.glob.failcheck == true) && (vars.glob.webfailcheck == true) && (System.IO.File.Exists(vars.glob.var_webcheck)))
                    {

                        item.SubItems.Add("Pass");
                        item.ForeColor = Color.Green;


                    }
                    else
                    {
                        if ((!System.IO.File.Exists(vars.glob.var_mediainfocheck)) | (!System.IO.File.Exists(vars.glob.var_hashcheck)) | (!System.IO.File.Exists(vars.glob.var_mediaconchcheck)) | (!System.IO.File.Exists(vars.glob.var_movcheck)) | (!System.IO.File.Exists(vars.glob.var_web_mediainfocheck)) | (!System.IO.File.Exists(vars.glob.var_web_mediaconchcheck)) | (!System.IO.File.Exists(vars.glob.var_web_hashcheck)) | (vars.glob.failcheck == false) | (vars.glob.webfailcheck == false) | (!System.IO.File.Exists(vars.glob.var_webcheck)))
                        {
                            if (!System.IO.File.Exists(vars.glob.var_mediainfocheck))
                            {
                                m_mediainfo(vars.glob.var_master_file);

                            }
                            if (!System.IO.File.Exists(vars.glob.var_hashcheck))
                            {
                                m_hash(vars.glob.var_master_file);
                            }
                            if (!System.IO.File.Exists(vars.glob.var_mediaconchcheck))
                            {
                                m_mediaconch(vars.glob.var_master_file);
                            }



                            if (!System.IO.File.Exists(vars.glob.var_web_mediainfocheck))
                            {
                                m_mediainfo(vars.glob.final_web_path);

                            }
                            if (!System.IO.File.Exists(vars.glob.var_web_hashcheck))
                            {
                                m_hash(vars.glob.final_web_path);
                            }
                            if (!System.IO.File.Exists(vars.glob.var_web_mediaconchcheck))
                            {
                                m_mediaconch(vars.glob.final_web_path);
                            }



                            if (vars.glob.failcheck == false)
                            {
                                m_mediaconch(vars.glob.var_master_file);
                                vars.glob.failcheck = (m_mediaconch_check(vars.glob.var_master_file_noext));
                            }

                            if (vars.glob.webfailcheck == false)
                            {
                                m_mediaconch(vars.glob.final_web_path);
                                vars.glob.webfailcheck = (m_mediaconch_check(vars.glob.var_web_file_noext));
                            }




                            if (!System.IO.File.Exists(vars.glob.var_webcheck))
                            {
                                m_web_encode(vars.glob.var_master_file, vars.glob.final_web_path);
                            }






                            if ((System.IO.File.Exists(vars.glob.var_mediainfocheck)) && (System.IO.File.Exists(vars.glob.var_hashcheck)) && (System.IO.File.Exists(vars.glob.var_movcheck)) && (System.IO.File.Exists(vars.glob.var_mediaconchcheck)) && (System.IO.File.Exists(vars.glob.var_web_mediainfocheck)) && (System.IO.File.Exists(vars.glob.var_web_mediaconchcheck)) && (System.IO.File.Exists(vars.glob.var_web_hashcheck)) && (vars.glob.failcheck == true) && (vars.glob.webfailcheck == true) && (System.IO.File.Exists(vars.glob.var_webcheck)))
                            {
                                item.SubItems.Add("Pass");
                                item.ForeColor = Color.Green;
                            }
                            else
                            {


                                if (!System.IO.File.Exists(vars.glob.var_movcheck))
                                {
                                    vars.glob.details += " Master file is Missing ---";
                                }

                                if (!System.IO.File.Exists(vars.glob.var_mediainfocheck))
                                {
                                    vars.glob.details += " Master Mediainfo Missing ---";
                                }
                                if (!System.IO.File.Exists(vars.glob.var_hashcheck))
                                {
                                    vars.glob.details += " Master Hash Missing ---";
                                }
                                if (!System.IO.File.Exists(vars.glob.var_mediaconchcheck))
                                {
                                    vars.glob.details += " Master Mediaconch Missing ---";
                                }
                                if (!System.IO.File.Exists(vars.glob.var_webcheck))
                                {
                                    vars.glob.details += " Web File Missing ---";
                                }

                                if (!System.IO.File.Exists(vars.glob.var_mediainfocheck))
                                {
                                    vars.glob.details += " Web Mediainfo Missing ---";
                                }
                                if (!System.IO.File.Exists(vars.glob.var_web_hashcheck))
                                {
                                    vars.glob.details += " Web Hash Missing ---";
                                }
                                if (!System.IO.File.Exists(vars.glob.var_web_mediaconchcheck))
                                {
                                    vars.glob.details += " Web Mediaconch Missing ---";
                                }
                                if (vars.glob.failcheck == false)
                                {
                                    vars.glob.details += " Master Media Conch QA check failed ---";
                                }
                                if (vars.glob.webfailcheck == false)
                                {
                                    vars.glob.details += " Web Media Conch QA check failed ---";
                                }






                                item.SubItems.Add("Fail");
                                item.ForeColor = Color.Red;

                                if (vars.glob.details.Length > 3)
                                {
                                    vars.glob.finaldetails = vars.glob.details.Substring(0, vars.glob.details.Length - 3);
                                    item.SubItems.Add(vars.glob.finaldetails);
                                }
                                else
                                {
                                    item.SubItems.Add(vars.glob.details);
                                }

                            }


                        }


                        else
                        {
                            item.SubItems.Add("Fail");
                            item.ForeColor = Color.Red;


                        }



                    }



                }
                m_statuschange("Off");

            }

            else
            {
                if (!System.IO.File.Exists(Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_ffmpeg_path))
                {
                    System.Windows.Forms.MessageBox.Show("FFMPEG not found");
                }
                if (!System.IO.File.Exists(Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_mediaconch_path))
                {
                    System.Windows.Forms.MessageBox.Show("Mediaconch not found");
                }
                if (!System.IO.File.Exists(Properties.Settings.Default.var_tools_path + Properties.Settings.Default.var_mediainfo_path))
                {
                    System.Windows.Forms.MessageBox.Show("Mediainfo not found");
                }
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.var_delete_box = checkBox1.Checked;
            vars.glob.var_del_check_bool = checkBox1.Checked;
        }
    }
}

