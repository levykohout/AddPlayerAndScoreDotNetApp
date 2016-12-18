using System.Collections.Generic; //needed to use List
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication.Models
{
    //creates Player object
    public class Player
    {
        //creates Player properties
        public virtual int Id { get; set; }
        public virtual string Name {get; set;}
        public virtual List<Score> Scores {get;set;} = new List<Score>();

        [NotMapped]
        [Display(Name="Total Score")]
        public virtual int TotalScore { get { return Scores.Sum(e => e.Value); } }
    }
}