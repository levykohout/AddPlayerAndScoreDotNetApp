

namespace WebApplication.Models
{

    //create Score object
    public class Score
    {
        //creates properties of Score object virtual is used to avoid infinite loop between Player and Score
        public virtual int Id {get; set;}
        public virtual int Value {get;set;}

        //first Player reference to Player object and second Player is property name
        public virtual Player Player {get;set;}
    }
}