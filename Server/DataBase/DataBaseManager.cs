using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace Server.DataBase
{
    public class DataBaseManager
    {
        private const string dataBasePath = @"\DataBase.txt";
        private const string archivePath = @"\Archive.txt";
        private static int num;
        private const char separator = ';';


        static DataBaseManager()
        {
            num = 0;
            DataBaseManager.CreateDB();
        }


        #region operations on DataBase
        private static void CreateDB()
        {
            try
            {
                if (!File.Exists(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath))
                {
                    var stream = File.CreateText(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);
                    stream.Close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public static bool ArchiveDB()
        {
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            string line = String.Empty;
            string[] lines = archivePath.Split('.');
            string tempPath = lines[0] + num++.ToString() + "." + lines[1];


            try
            {

                //while (true)
                //{
                //    if (file.exists(directoryconfig.instance.archivedirectory + temppath))
                //    {
                //        temppath = lines[0] + (++num).tostring() + "." + lines[1];
                //    }
                //    else
                //    {
                //        break;
                //    }

                //}


                streamReader = new StreamReader(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);
                streamWriter = File.CreateText(DirectoryConfig.Instance.ArchiveDirectory + tempPath);

                while ((line = streamReader.ReadLine()) != null)
                {
                    streamWriter.WriteLine(line);
                }
                streamReader.Close();
                DeleteDB();
                streamWriter.Close();

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        public static bool DeleteDB()
        {

            try
            {
                if (File.Exists(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath))
                {
                    // File.Delete(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);

                    using (File.Create(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath))
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region operations on Entities
        public static string InsertEntity(string id, string user, string consumption)
        {

            StreamReader streamReader = null;
            StreamWriter streamWriter = null;

            string line = String.Empty;
            string[] lines = new string[3];
            bool exist = false;

            try
            {
                using (streamReader = new StreamReader(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines = line.Split(separator);

                        if (lines[0].Equals(id))
                        {
                            exist = true;
                            break;
                        }
                    }
                }
                if (exist)
                {
                    return String.Format("Entity with id {0} already exists.",id);
                }
                else
                {
                    streamWriter = File.AppendText(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);
                    lines[0] = id;
                    lines[1] = user;
                    lines[2] = consumption;

                    streamWriter.WriteLine(String.Join(separator.ToString(), lines));



                    return string.Empty;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return String.Format("Inserting entity with id {0} faild.", id);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }

                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
        }

        public static bool ModifiyEntityId(string id, string newID)
        {
            StreamReader streamReader = null;
            StringBuilder stringBuilder = new StringBuilder();
            string line = String.Empty;
            string[] lines = new string[3];
            string[] tempLines = new string[3];

            try
            {
                streamReader = new StreamReader(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);

                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(separator);

                    if (lines[0].Equals(id))
                    {
                        tempLines[0] = newID;
                        tempLines[1] = lines[1];
                        tempLines[2] = lines[2];

                        continue;
                    }
                    stringBuilder.AppendLine(line);
                }

                if (tempLines[0].Equals(String.Empty))
                {
                    return false;
                }

                stringBuilder.AppendLine(String.Join(separator.ToString(), tempLines));
                streamReader.Close();

                File.WriteAllText(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath, stringBuilder.ToString());

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
        }

        public static bool ModifiyEntity(string id, string consumption)
        {
            StreamReader streamReader = null;
            StringBuilder stringBuilder = new StringBuilder();
            string line = String.Empty;
            string[] lines = new string[3];
            string[] tempLines = new string[3];

            try
            {
                streamReader = new StreamReader(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);

                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(separator);

                    if (lines[0].Equals(id))
                    {
                        tempLines[0] = lines[0];
                        tempLines[1] = lines[1];
                        tempLines[2] = consumption;

                        continue;
                    }
                    stringBuilder.AppendLine(line);
                }

                if (tempLines[0].Equals(String.Empty))
                {
                    return false;
                }

                stringBuilder.AppendLine(String.Join(separator.ToString(), tempLines));
                streamReader.Close();

                File.WriteAllText(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath, stringBuilder.ToString());

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
        }



        public static bool DeleteEntity(string id)
        {
            StreamReader streamReader = null;
            StringBuilder stringBuilder = new StringBuilder();
            string line = string.Empty;
            string[] lines = new string[3];
            bool retVal = false;

            try
            {
                streamReader = new StreamReader(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);

                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(separator);

                    if (lines[0].Equals(id))
                    {
                        retVal = true;
                        continue;
                    }
                    stringBuilder.AppendLine(line);
                }
                streamReader.Close();

                File.WriteAllText(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath, stringBuilder.ToString());

                return retVal;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }

        }

        public static SmartMeter GetEntity(string id)
        {
            SmartMeter smartMeter = null;
            StreamReader streamReader = null;
            string line = String.Empty;
            string[] lines = new string[3];

            try
            {
                streamReader = new StreamReader(DirectoryConfig.Instance.DataBaseDirecotry + dataBasePath);

                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(separator);

                    if (lines[0].Equals(id))
                    {

                        smartMeter = new SmartMeter(Int32.Parse(id), lines[1], float.Parse(lines[2]));
                        break;
                    }
                }


                return smartMeter;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return smartMeter;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }


        }
        #endregion
    }

}

