using System.Collections.Generic;
using ClearCode.Data.Entities;

namespace ClearCode.Services
{
    public interface IVotesService
    {
        bool CreateAnswer(AnswerEntity answer);
        bool UpdateAnswer(AnswerEntity answer);
        bool DeleteAnswer(AnswerEntity answer);
        AnswerEntity FindAnswerById(string id);
        List<AnswerEntity> FindAllAnswer();
        bool CreateUser(UserEntity user);
        bool UpdateUser(UserEntity user);
        bool DeleteUser(UserEntity user);
        Results Tally(string[][] votes);
        List<UserEntity> FindAllUser();
        UserEntity FindUserById(string id);
        bool CreateStat(StatEntity stat);
        List<Stat> FindAllStat();
        StatEntity FindStatById(string Id);
        List<Stat> GetStatByPoll(string Id);
    }
}