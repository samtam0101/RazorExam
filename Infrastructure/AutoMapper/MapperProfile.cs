using AutoMapper;
using Domain.DTOs.AccountDTO;
using Domain.DTOs.CustomerDTO;
using Domain.DTOs.TransactionDTO;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Account, CreateAccountDto>().ReverseMap();
        CreateMap<Account, UpdateAccountDto>().ReverseMap();
        CreateMap<Account, GetAccountsDto>().ReverseMap();

        CreateMap<Customer, CreateCustomerDto>().ReverseMap();
        CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
        CreateMap<Customer, GetCustomersDto>().ReverseMap();

        CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
        CreateMap<Transaction, GetTransactionsDto>().ReverseMap();
    }
}
