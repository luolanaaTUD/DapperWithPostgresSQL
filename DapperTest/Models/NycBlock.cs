using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using NetTopologySuite.Geometries;

namespace DapperTest.Models
{
	public class NycBlock
	{
        [Key]
        public int Id { get; set; }

        public string? Blkid { get; set; }

        public string? Boroname { get; set; }        

        public int Popn_total { get; set; }

        public int Popn_white { get; set; }

        public int Popn_black { get; set; }

        public MultiPolygon? Geom { get; set; }
    }
}

