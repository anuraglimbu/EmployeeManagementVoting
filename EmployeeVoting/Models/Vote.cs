using System.ComponentModel.DataAnnotations;

namespace EmployeeVoting.Models
{
    public class Vote
    {
        [Key]
        public int vote_id { get; set; }

        public DateTime vote_date { get; set; }

        public int voter_id { get; set; }

        public int candidate_id { get; set;}
    }
}
