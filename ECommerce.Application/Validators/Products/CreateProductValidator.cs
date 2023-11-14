using FluentValidation; //Kütüphanesi kullanıyoruz validasyonlar için
public class CreateProductValidator:AbstractValidator<VM_Create_Product>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
            .MaximumLength(150)
            .MinimumLength(3)
                .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında geçmeyiniz.");

        RuleFor(p => p.Stock)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen stok bilgisini boş geçmeyiniz.")
            .Must(s => s >= 0) // 0'a eşit veya büyük olma durumu
                .WithMessage("Sstok bilgisi negatif değer alamaz.");

        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.")
            .Must(s => s >= 0) // 0'a eşit veya büyük olma durumu
                .WithMessage("Fiyat bilgisi negatif değer alamaz.");

    }
}
