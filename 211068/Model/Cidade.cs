using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace _211068.Model
{
    internal class Cidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }
    }
}
