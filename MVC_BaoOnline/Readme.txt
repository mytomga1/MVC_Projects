	XÂY DỰNG BLOG VỚI ASP.NET CORE 5.0 MVC

I - Lựa chọn Template
II - Thiết kế database
III - Xây dựng layout cho website
		_  design layout cho admin
		_ design layout cho user
IV - Xây dựng các task quản lý (admin)
		_ Quản lý tin tức
			+ thêm
			+ xoá
			+ sửa
			+ tìm kiếm
			+ DS tin tức
		_ Quản lý danh mục
			+ thêm danh mục
			+ xoá danh mục
			+ tìm kiếm danh mục
			+ danh sách danh mục
		_ Quản lý  tài khoản
			+ Login / Logout
			+ add / delete/ edit 
			+ danh sách tài khoản
V - Xây dựng giao diện người dùng
		_  hiển thị danh mục
		_  hiển thị quảng cáo
		_  Giao diện index
		_  Giao diện danh mục
		_  Giao diện tin tức
		_  Phân trang
		_  Xử lý cache
VI -  Cấu Hình Seo
		_ Tạo sitemap cho website tự động

---------------------------------------------------------------------------------------
Các thư viện sử dụng : 
 
Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation -Version 5.0.9
 Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.9
 Install-Package Microsoft.EntityFrameworkCore.SqlServer  -Version 5.0.9
 Install-Package Microsoft.EntityFrameworkCore -Version 5.0.9
 PagedList.Core
 
 Scaffold-DbContext  "Server=ADMIN-PC; Database=BaoOnline; Integrated Security=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Mode