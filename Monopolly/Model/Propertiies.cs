using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopolly.Model
{
	internal class Propertiies
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Tag { get; set; }

		public Propertiies(int id, string name, string tag)
		{
			Id = id;
			Name = name;
			Tag = tag;
		}
	}
}
