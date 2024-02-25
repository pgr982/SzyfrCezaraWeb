using Microsoft.AspNetCore.Mvc;
using SzyfrCezaraWeb.Models;

namespace SzyfrCezaraWeb.Controllers
{
    public class CipherController : Controller
    {
        private readonly char[] polishAlphabetSmall = {
            'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
            'ł', 'm', 'n', 'ń', 'o', 'ó', 'p','q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż'
        };
        private readonly char[] polishAlphabetCapital = {
            'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
            'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'Q' ,'R', 'S', 'Ś', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ź', 'Ż'
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Encrypt(CipherModel model)
        {
            if (ModelState.IsValid)
            {
                string encryptedText = EncryptText(model.Text, model.Key);
                ViewBag.EncryptedText = encryptedText;
                return View("Index", model);
            }
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Decrypt(CipherModel model)
        {
            if (ModelState.IsValid)
            {
                string decryptedText = DecryptText(model.Text, model.Key);
                ViewBag.DecryptedText = decryptedText;
                return View("Index", model);
            }
            return View("Index", model);
        }

        private string EncryptText(string text, int key)
        {
            string encryptedText = "";
            int smallAlphabetLength = polishAlphabetSmall.Length;
            int capitalAlphabetLength = polishAlphabetCapital.Length;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    if (!char.IsUpper(c))
                    {
                        int index = Array.IndexOf(polishAlphabetSmall, c);
                        int shiftedIndex = (index + (key % smallAlphabetLength)) % smallAlphabetLength;
                        encryptedText += polishAlphabetSmall[shiftedIndex];
                    }
                    else
                    {
                        int index = Array.IndexOf(polishAlphabetCapital, c);
                        int shiftedIndex = (index + (key % capitalAlphabetLength)) % capitalAlphabetLength;
                        encryptedText += polishAlphabetCapital[shiftedIndex];
                    }
                }
                else
                {
                    encryptedText += c;
                }
            }
            return encryptedText;
        }

        private string DecryptText(string text, int key)
        {
            string decryptedText = "";
            int smallAlphabetLength = polishAlphabetSmall.Length;
            int capitalAlphabetLength = polishAlphabetCapital.Length;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    if (!char.IsUpper(c))
                    {
                        int index = Array.IndexOf(polishAlphabetSmall, c);
                        int shiftedIndex = (index - (key % smallAlphabetLength) + smallAlphabetLength) % smallAlphabetLength;
                        decryptedText += polishAlphabetSmall[shiftedIndex];
                    }
                    else
                    {
                        int index = Array.IndexOf(polishAlphabetCapital, c);
                        int shiftedIndex = (index - (key % capitalAlphabetLength) + capitalAlphabetLength) % capitalAlphabetLength;
                        decryptedText += polishAlphabetCapital[shiftedIndex];
                    }
                }
                else
                {
                    decryptedText += c;
                }
            }
            return decryptedText;
        }
    }
}