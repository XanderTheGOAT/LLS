using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightLinkModels
{
    public class Computer
    {
        private string name;
        private string userName;
        private string[] connectedDevices;

        public string Name { get => name ?? ""; set => name = value; }
        public string UserName { get => userName ?? ""; set => userName = value; }
        public string[] ConnectedDevices { get => connectedDevices.ToArray(); set => connectedDevices = value; }
    }
}
