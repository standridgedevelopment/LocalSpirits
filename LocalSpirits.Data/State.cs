using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public enum StateName
    {
        Alabama, Alaska, Arizona, Arkansas, California, Colorado, Conneticut, Delaware, Florida, Georgia, hawaii, Idaho, Illinois, Indiana, Iowa, Kansas, Kentucky, Louisiana, Maine, Maryland, massachusetts, Michigan, Minnesota, Mississippi, Missouri, Montana, Nebraska, Nevada, New_Hampshire, New_Jeresy, New_Mexico, New_York, North_Carolina, North_Dakota, Ohio, Oklahoma, Oregon, Pennsylvania, Rhode_Island, South_Carolina, South_Dakota, Tennessee, Texas, Utah, Vermont, Virginia, Washington, West_Virginia, Wisconsin, Whyoming
    }
    public class State
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
