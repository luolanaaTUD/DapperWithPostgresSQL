using System;
namespace DapperTest.Dtos
{
	public class CreateNycBlockDto
	{
        public int Id { get; set; }

        public string? Blkid { get; set; }

        public string? Boroname { get; set; }

        public int Popn_total { get; set; }

        public int Popn_white { get; set; }

        public int Popn_black { get; set; }
    }
}

