﻿using System.Threading.Tasks;
using ygo.domain.Models;

namespace ygo.application.Repository
{
    public interface ICardRepository
    {
        Task<Card> CardByName(string name);
        Task<Card> Add(Card newCard);
        Task<Card> CardById(long id);
        Task<Card> Update(Card card);
        Task<bool> CardExists(long id);
    }
}