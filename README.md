*Turkish Readme*

# UserModuleTemplate

Bu proje, .NET 9 ile geliştirilmiş, modüler yapıya sahip bir kullanıcı yönetim modülüdür.

## Özellikler
- Kullanıcı Kaydı (Register)
- Giriş / Çıkış (Login / Logout)
- Şifre Yenileme
- JWT + Refresh Token
- E-posta Doğrulama
- Rol Bazlı Yetkilendirme
- Serilog ile Loglama (Database dahil)
- Global Exception Handling Middleware

## Kullanım
1. `appsettings.json` içine kendi `Jwt:Key`, `SmtpSettings` vs. bilgilerini gir.
2. Migration ve `update-database` işlemlerini çalıştır.
3. Uygulamayı başlat.

## Notlar
Bu repo, başka projelere entegre edilmek üzere hazır bir `UserModule` altyapısıdır.

---

*English Readme*

# UserModuleTemplate

This project is a modular user management module built with .NET 9.

## Features
- User Registration
- Login / Logout
- Password Reset
- JWT + Refresh Token
- Email Verification
- Role-Based Authorization
- Logging with Serilog (including database)
- Global Exception Handling Middleware

## Usage
1. Fill in your own `Jwt:Key`, `SmtpSettings`, etc. inside `appsettings.json`.
2. Run the migration and `update-database` commands.
3. Start the application.

## Notes
This repository is a ready-to-integrate `UserModule` infrastructure for use in other projects.
