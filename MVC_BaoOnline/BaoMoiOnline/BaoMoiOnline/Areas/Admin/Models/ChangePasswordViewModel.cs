using System.ComponentModel.DataAnnotations;

namespace BaoMoiOnline.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name = "Mật khẩu hiện tại")]
        public string PasswordNow { get; set; }

        [Display(Name = "Mật khẩu Mới")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Bạn cần đặt Mật khẩu tối thiểu 6 ký tự")]

        public string Password { get; set; }
        [MinLength(6, ErrorMessage = "Bạn cần đặt Mật khẩu tối thiểu 6 ký tự")]
        [Display(Name = "Nhập lại Mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Mật khẩu không giống nhau")]
        public string ConfirmPassword { get; set; }
    }
}