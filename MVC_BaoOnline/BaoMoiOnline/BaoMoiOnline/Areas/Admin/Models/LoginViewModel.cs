using System.ComponentModel.DataAnnotations;

namespace BaoMoiOnline.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập lại Email")]
        [Display(Name = "Địa chỉ Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập lại Email")]
        public string Email { get; set; }


        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [MaxLength(30, ErrorMessage = "Mật khẩu chỉ dc sử dụng tối đa 30 ký tự")]
        public string Password { get; set; }
    }
}