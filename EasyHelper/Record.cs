using System.Windows.Forms;

namespace EasyHelper
{
    public class Record
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public string Arguments { get; set; }
        public Keys Key { get; set; }
        public Mdificator Modificator { get; set; }
    }
}
