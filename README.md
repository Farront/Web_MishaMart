# Web_MishaMart
Trang web bán trò chơi điện tử sử dụng C#

--Copy file .mdf và .log vào database (Hoặc sử dụng query để xây CSDL) (C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA)
--Thay đổi connectionString trong Web.Config của file .sln 
	 --- Code ban đầu: ---
	<connectionStrings>
		<add name="MyDatabaseConnection" connectionString="Server=DESKTOP-FCK5SHU\SQLEXPRESS;Database=MISHADB;Integrated Security=True;" providerName="System.Data.SqlClient" />
	</connectionStrings>
		Thay đổi (connectionString="Server=DESKTOP-FCK5SHU\SQLEXPRESS) thành tên server sử dụng hiện tại. 
--Chạy chương trình thông qua Default.aspx


-- Tài khoản Quản trị: admin@example.com, hashed_password_1 
-- Tài khoản người dung : user@example.com, hashed_password_2
