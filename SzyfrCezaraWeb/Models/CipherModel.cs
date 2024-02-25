
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SzyfrCezaraWeb.Models
{
    public class CipherModel
    {
        [DisplayName("Tekst")]
        [Required(ErrorMessage = "Wprowadź tekst do zaszyfrowania/oszyfrowania")]
        public string Text { get; set; }

        [DisplayName("Klucz")]
        [Required(ErrorMessage = "Klucz szyfrowania jest wymagany")]
        [Range(0, int.MaxValue, ErrorMessage = "Kluczem nie może być liczba ujemna")]
        public int Key { get; set; }

    }
}
