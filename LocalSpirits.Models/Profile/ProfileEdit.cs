using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Profile
{
    
    public class ProfileEdit
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public StateName State { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [DisplayName("Enter Number Of Profile Picture")]
        [Range(1, 7)]
        public int ProfilePicture { get; set; }
        public string GetProfilePicture
        {
            get
            {
                if (ProfilePicture == 1) return "~/assets/ProfileIcons/bird.png";
                if (ProfilePicture == 2) return "~/assets/ProfileIcons/clown-fish.png";
                if (ProfilePicture == 3) return "~/assets/ProfileIcons/cocker-spaniel.png";
                if (ProfilePicture == 4) return "~/assets/ProfileIcons/dinosaur.png";
                if (ProfilePicture == 5) return "~/assets/ProfileIcons/parrot.png";
                if (ProfilePicture == 6) return "~/assets/ProfileIcons/penguin.png";
                if (ProfilePicture == 7) return "~/assets/ProfileIcons/seal.png";
                return null;

            }
            set { }
        }
    }
}
