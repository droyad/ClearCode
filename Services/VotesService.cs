using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Data;
using ClearCode.Data.Entities;

namespace ClearCode.Services
{
    [InstancePerDependency]
    public class VotesService : IVotesService
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly ICandidateRepository _candidateRepository;

        public VotesService(IPreferenceRepository preferenceRepository, ICandidateRepository candidateRepository)
        {
            _preferenceRepository = preferenceRepository;
            _candidateRepository = candidateRepository;
        }

        public bool CreateAnswer(AnswerEntity answer)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.AnswerEntities
                                 select info);

                    entity.AnswerEntities.Add(answer);
                    entity.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAnswer(AnswerEntity answer)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from Info in entity.AnswerEntities
                                 select Info);

                    if (query.Any())
                    {
                        entity.AnswerEntities.Remove(query.First());
                        entity.SaveChanges();

                        entity.AnswerEntities.Add(answer);
                        entity.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAnswer(AnswerEntity answer)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from details in entity.AnswerEntities
                                 where details.AnswerId == answer.AnswerId &&
                                 details.PollId == answer.PollId && details.Title == answer.Title
                                 select details);

                    if (query.Any())
                    {
                        entity.AnswerEntities.Remove(query.First());
                        entity.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AnswerEntity FindAnswerById(string id)
        {
            try
            {
                AnswerEntity answer = new AnswerEntity();
                int nid = Convert.ToInt32(id);
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.AnswerEntities
                                 where info.AnswerId == nid
                                 select info);

                    if (query.Any())
                        answer = query.First();

                };
                return answer;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<AnswerEntity> FindAllAnswer()
        {
            try
            {
                List<AnswerEntity> answer = new List<AnswerEntity>();
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.AnswerEntities
                                 select info);

                    if (query.Any())
                        answer = query.ToList();
                };
                return answer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateUser(UserEntity user)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.UserEntities
                                 select info);

                    entity.UserEntities.Add(user);
                    entity.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateUser(UserEntity user)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from Info in entity.UserEntities
                                 select Info);

                    if (query.Any())
                    {
                        entity.UserEntities.Remove(query.First());
                        entity.SaveChanges();

                        entity.UserEntities.Add(user);
                        entity.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteUser(UserEntity user)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from details in entity.UserEntities
                                 where details.UserId == user.UserId &&
                                 details.UserName == user.UserName && details.UserPassword == user.UserPassword
                                 && details.UserEmail == user.UserEmail
                                 select details);

                    if (query.Any())
                    {
                        entity.UserEntities.Remove(query.First());
                        entity.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public UserEntity FindUserById(string id)
        {
            try
            {
                UserEntity user = new UserEntity();
                int nid = Convert.ToInt32(id);
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.UserEntities
                                 where info.UserId == nid
                                 select info);

                    if (query.Any())
                        user = query.First();

                };
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Statistic Creation
        public bool CreateStat(StatEntity stat)
        {
            try
            {
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.StatEntities
                                 select info);

                    entity.StatEntities.Add(stat);
                    entity.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Finding All Statistics 
        public List<Stat> FindAllStat()
        {
            try
            {
                List<Stat> stat = new List<Stat>();
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.StatEntities
                                 select info);

                    if (query.Any())
                        stat = query.ToList();
                };
                return stat;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Finding Statistics based on Id
        public StatEntity FindStatById(string Id)
        {
            try
            {
                StatEntity stat = new StatEntity();
                int nid = Convert.ToInt32(Id);
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.StatEntities
                                 where info.StatId == (object)nid
                                 select info);

                    if (query.Any())
                        stat = (StatEntity)query.First();

                };
                return stat;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Stat> GetStatByPoll(string Id)
        {
            try
            {
                List<Stat> stat = new List<Stat>();
                int nid = Convert.ToInt32(Id);
                using (EVoteEntities entity = new EVoteEntities())
                {
                    var query = (from info in entity.StatEntities
                                 where info.PollId == nid
                                 select new Stat
                                 {
                                     StatId = info.StatId,
                                     AnswerOneId = info.AnswerOneId,
                                     AnswerTwoId = info.AnswerTwoId,
                                     AnswerThreeId = info.AnswerThreeId,
                                     AnswerFourId = info.AnswerFourId,
                                 }).ToList();

                    stat = query;

                };
                return stat;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
