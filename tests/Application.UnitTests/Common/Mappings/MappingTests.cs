using AutoMapper;
using Jorda.Application.Common.Mappings;
using Jorda.Application.CQRS.Goal.Queries.DTOs;
using Jorda.Application.CQRS.Note.Queries.DTOs;
using Jorda.Application.CQRS.Section.Queries.DTOs;
using Jorda.Application.CQRS.ToDoItem.Queries.DTOs;
using Jorda.Server.Application.Common.Mappings;
using Jorda.Server.Application.CQRS.HistoryItem.Queries.DTOs;
using Jorda.Server.Application.CQRS.Label.Queries.DTOs;
using Jorda.Server.Application.CQRS.Notification.Queries.DTOs;
using Jorda.Server.Application.CQRS.UserFile.Queries.DTOs;
using Jorda.Server.Domain.Entities;
using System.Runtime.Serialization;


namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<GoalProfile>();
                config.AddProfile<HistoryItemProfile>();
                config.AddProfile<LabelProfile>();
                config.AddProfile<NoteProfile>();
                config.AddProfile<NotificationProfile>();
                config.AddProfile<SectionProfile>();
                config.AddProfile<ToDoItemProfile>();
                config.AddProfile<UserFileProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }


        [Theory]
        [InlineData(typeof(Goal), typeof(GoalDTO))]
        [InlineData(typeof(HistoryItem), typeof(HistoryItemDTO))]
        [InlineData(typeof(Label), typeof(LabelDTO))]
        [InlineData(typeof(Note), typeof(NoteDTO))]
        [InlineData(typeof(Notification), typeof(NotificationDTO))]
        [InlineData(typeof(Section), typeof(SectionDTO))]
        [InlineData(typeof(ToDoItem), typeof(ToDoItemDTO))]
        [InlineData(typeof(UserFile), typeof(UserFileDTO))]
        [InlineData(typeof(UserFile), typeof(UserFileDetailsDTO))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }


    }
}
