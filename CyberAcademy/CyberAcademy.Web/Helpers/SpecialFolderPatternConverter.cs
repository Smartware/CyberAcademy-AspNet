using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web.Helpers
{
    public class SpecialFolderPatternConverter : log4net.Util.PatternConverter
    {
        override protected void Convert(System.IO.TextWriter writer, object state)
        {
            //Environment.SpecialFolder specialFolder = (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), base.Option, true);
            string homePath = HttpRuntime.AppDomainAppPath;
            //writer.Write(Environment.GetFolderPath(specialFolder));
            writer.Write(homePath);
        }
    }
}