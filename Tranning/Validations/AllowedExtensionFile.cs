using System.ComponentModel.DataAnnotations;

namespace Tranning.Validations
{
    public class AllowedExtensionFile : ValidationAttribute
    {
        private readonly string[] _extension;
        public AllowedExtensionFile(string[] extensions)
        {
            _extension = extensions;
        }
        protected override ValidationResult? IsValid(
            object value, 
            ValidationContext validationContext
            ){
            var file = value as IFormFile;
            if( file != null ) 
            {
                var extensions = Path.GetExtension(file.FileName);
                if  (!_extension.Contains( extensions.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;

        }

        private string GetErrorMessage()
        {
            return "This photo extension is not allowed";
        }
    }
}
