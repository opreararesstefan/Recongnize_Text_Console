using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recongnize_Text_Console.Controllers
{
    public class NumberExpanded
    {
       
        #region Field

        /// <summary>
        /// NumberExpandDictionary
        /// </summary>
        public static Dictionary<string, char[][]> NumberExpandDictionary = new Dictionary<string, char[][]>()
        {
            { "NumberOne", new char[4][] { new char[] { '\0', '|' }, new char[] { '\0', '|' }, new char[] { '\0', '|' }, new char[] { '\0', '|' } } },
            { "NumberTwo", new char[4][] { new char[] { '-', '-', '-' }, new char[] { '\0', '_', '|' }, new char[] { '|', '\0', '\0' }, new char[] { '-', '-', '-' } } },
            { "NumberThree", new char[4][] { new char[] { '-', '-', '-' }, new char[] { '\0', '/', '\0' }, new char[] { '\0', '\\', '\0' }, new char[] { '-', '-', '\0' } }  },
            { "NumberFour", new char[4][] { new char[] { '|', '\0', '\0', '\0', '|' }, new char[] { '|', '_', '_', '_', '|' }, new char[] { '\0', '\0', '\0', '\0', '|' }, new char[] { '\0', '\0', '\0', '\0', '|' } }  },
            { "NumberFive", new char[4][] { new char[] { '-', '-', '-', '-', '-' }, new char[] { '|', '_', '_', '_', '\0' }, new char[] { '\0', '\0', '\0', '\0', '|' }, new char[] { '_', '_', '_', '_', '|' } }  }
        };

        #endregion

    }
}
