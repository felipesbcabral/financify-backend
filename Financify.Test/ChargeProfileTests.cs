using AutoMapper;
using Financify_Api.Models.Enums;
using Financify_Api.Models.Responses;
using Financify_Api.Models;
using Financify_Api.Models.Profiles;
using Financify_Api.Models.Enums.Extensions;

namespace Financify.Test
{
    public class ChargeProfileTests
    {
        private IMapper _mapper;

        [Fact]
        public void ChargeProfile_Mapping_IsValid()
        {

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ChargeProfile>();
            });

            _mapper = configuration.CreateMapper();

            // Arrange
            var charge = new Charge
            {
                Id = Guid.NewGuid(),
                Name = "Teste",
                Value = 100,
                Status = ChargeStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            // Act
            var result = _mapper.Map<ChargeResponse>(charge);

            // Assert
            Assert.Equal(charge.Id, result.Id);
            Assert.Equal(charge.Name, result.Name);
            Assert.Equal(charge.Value, result.Value);
            Assert.Equal(charge.Status.GetDescription(), result.Status);
        }
    }
}