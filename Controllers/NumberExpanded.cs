using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recongnize_Text_Console.Controllers
{
    public class NumberExpanded
    {
        public NumberExpanded()
        { }

        public Dictionary<string, char[,]> NumberExpand = new Dictionary<string, char[,]>()
        { 
            { "NumberOne", new char[4,2] { { '\0', '|' }, {'\0', '|' }, { '\0', '|' }, {'\0', '|' } } },
            { "NumberTwo", new char[4,3] { { '-', '-', '-' }, {'\0', '_', '|' }, { '|', '\0', '\0' }, { '-', '-', '-' } } },
            { "NumberThree", new char[4,3] { { '-', '-', '-' }, {'\0', '/', '\0' }, { '\0', '\\', '\0' }, { '-', '-', '\0' } }  },
            { "NumberFour", new char[4,5] { { '|', '\0', '\0', '\0', '|' }, { '|', '_', '_', '_', '|' }, { '\0', '\0', '\0', '\0', '|' }, { '\0', '\0', '\0', '\0', '|' } }  },
            { "NumberFive", new char[4,5] { { '-', '-', '-', '-', '-' }, { '|', '_', '_', '_', '\n' }, { '\0', '\0', '\0', '\0', '|' }, { '_', '_', '_', '_', '|' } }  }
        };

    }
}
