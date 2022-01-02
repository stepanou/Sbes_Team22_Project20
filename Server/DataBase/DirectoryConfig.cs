using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server.DataBase
{
    public class DirectoryConfig
    {
        private string dataBaseDirecotry = string.Empty;
        private string archiveDirectory = string.Empty;

        private static DirectoryConfig instance = null;


        public string DataBaseDirecotry
        {
            get { return dataBaseDirecotry; }
        }

        public string ArchiveDirectory { get => archiveDirectory; }

        public static DirectoryConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DirectoryConfig();
                }

                return instance;
            }
        }



        private DirectoryConfig()
        {
            dataBaseDirecotry = ConfigurationManager.AppSettings["DataBaseDirectory"];
            archiveDirectory = ConfigurationManager.AppSettings["ArchiveDirectory"];

            if (!Directory.Exists(dataBaseDirecotry))
            {
                Directory.CreateDirectory(dataBaseDirecotry);
            }
            if (!Directory.Exists(archiveDirectory))
            {
                Directory.CreateDirectory(archiveDirectory);
            }
        }

    }
}

