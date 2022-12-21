using FluentValidation;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Resources.Validation.UserFile;
using Microsoft.AspNetCore.Http;

namespace Jorda.Server.Application.CQRS.UserFile.Commands.CreateUserFileCommand;

public class CreateUserFileCommandValidator : AbstractValidator<CreateUserFileCommand>
{
    private string[] _allowedFileExtensions = new[]{ ".jpg",".jpeg",".png",".pdf"};
    private int _allowedFileSizeKB = 1024;

    public CreateUserFileCommandValidator()
    {


        RuleFor(v => v.File)
            .NotEmpty().WithMessage(UploadUserFileCommandValidator.UserFileEmpty)
            .MustAsync(HaveValidExtension).WithMessage(string.Format(UploadUserFileCommandValidator.FileWrongExtension, string.Join(",", _allowedFileExtensions)))
            .Must(HaveValidSize).WithMessage(string.Format(UploadUserFileCommandValidator.FileTooBig, _allowedFileSizeKB));
       
    }

    public async Task<bool> HaveValidExtension(IFormFile file, CancellationToken cancellationToken)
    {
        var valid = true;
        await Task.Run(()=>
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_allowedFileExtensions.Contains(extension.ToLower()))
            {
                valid = false;
            }
        });
        return valid;
    }

    public bool HaveValidSize(IFormFile file)
    {
        if (file.Length > _allowedFileSizeKB * 1000)
        {
            return false;
        }
        return true;
    }
}

