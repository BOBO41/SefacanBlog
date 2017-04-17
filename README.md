# SefacanBlog

Sefacan.net alan adında barındırdığım web sitesini açık kaynak olarak paylaşma kararı aldım. Modern .Net teknolojilerinin ve günümüz projelerinin ihtiyaç duyduğu bir yapıya sahip. Blog projesini bu seviyede yapmak ne kadar gerekli diyebilirsiniz. Amaç kendimizi her daim geliştirmek olduğu için böyle bir yapıyı herkesin denemesini öneriyorum. Artık geliştirdiğim ve geliştireceğim projelerde bu Tasarım Kalıbı üzerinden gitmeyi ve yeni eklemeler yapmayı düşünüyorum.

Kullandığım Kütüphane ve Teknolojiler

Asp.Net MVC 5

Autofac.MVC

Entityframework 6

Newtonsoft.JSON

MailKit

1. Projeyi indirip Visual Studio 2012 ve üzeri bir sürümle açın.

2. Web.Config dosyasına veritabanı yolunuzu verin.  

3. Database.sql dosyasını MSSQL Management Studio ile çalıştırın

4. Projeye dönüp Package Manager Console üzerinden Sefacan.Data projesini seçip update-database yazarak Web.config dosyasında verdiğiniz ConnectionString bilgisindeki veritabanını güncelleyin. 

5. Sefacan.Web projesini publish edip sunucuya atın.

6. Sefacan.Admin projesini publish edip herhangi bir subdomain açarak dosyaları sunucuya atın.

Web.config dosyasındaki Connection bilgisi hem Sefacan.Web içerisinde hemde Sefacan.Admin içerisinde aynı olmalıdır. Update-database sırasında connection bilgisi bulunamadı uyarısı alırsanız Sefacan.Web projesine sağ click yapıp Set as Startup diyip tekrar update-database yapın.
