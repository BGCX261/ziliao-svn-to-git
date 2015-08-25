using System;
using System.Collections.Generic;
using System.Text;

namespace TDK.Core.Logic.BLL
{
    public partial class BllManager
    {
        private string projectFileOutputPath;
        /// <summary>
        /// 工程文件输出根目录
        /// </summary>
        /// <example>
        /// \\data\xinhua
        /// </example>
        public string ProjectFileOutputPath
        {
            get
            {
                return projectFileOutputPath;
            }
            set
            {
                projectFileOutputPath = value;
            }
        }


    }
}
