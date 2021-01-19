using AutoMapper;
using MasAcademyLab.Data.Repositories;
using MasAcademyLab.Domain;
using MasAcademyLab.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasAcademyLab.Service
{
    public class TalkService : ITalkService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TalkService(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TalkModel>> GetTalksAsync(string code, bool includeSpeakers = false)
        {
            try
            {
                var talks = await _trainingRepository.GetTalksByCodeAsync(code, includeSpeakers);

                return _mapper.Map<IEnumerable<TalkModel>>(talks);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TalkModel> GetTalkAsync(string code, int talkId, bool includeSpeakers = false)
        {
            try
            {
                var talk = await _trainingRepository.GetTalkByCodeAsync(code, talkId, includeSpeakers);

                return _mapper.Map<TalkModel>(talk);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TalkModel> CreateTalkAsync(string code, TalkModel talkModel)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingAsync(code);

                if (training == null || talkModel.Speaker == null)
                {
                    return null;
                }

                talkModel.TalkId = 0;

                var talk = _mapper.Map<Talk>(talkModel);
                var speaker = await _trainingRepository.GetSpeakerAsync(talkModel.Speaker.SpeakerId);

                if (speaker == null)
                {
                    return null;
                }

                talk.Training = training;
                talk.Speaker = speaker;
                talk.SpeakerId = speaker.SpeakerId;

                _trainingRepository.Add(talk);

                await _trainingRepository.SaveChangesAsync();
                return _mapper.Map<TalkModel>(talk);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TalkModel> UpdateTalkAsync(string code, int talkId, TalkModel talkModel)
        {
            try
            {
                var oldTalk = await _trainingRepository.GetTalkByCodeAsync(code, talkId);

                if (oldTalk == null)
                {
                    return null;
                }

                _mapper.Map(talkModel, oldTalk);

                if (talkModel.Speaker != null)
                {
                    var speaker = await _trainingRepository.GetSpeakerAsync(talkModel.Speaker.SpeakerId);
                    if(speaker != null)
                    {
                        oldTalk.Speaker = speaker;
                    }
                }
                await _trainingRepository.SaveChangesAsync();
                return _mapper.Map(oldTalk, talkModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteTalkAsync(string code, int talkId)
        {
            var oldTalk = await _trainingRepository.GetTalkByCodeAsync(code, talkId);

            if (oldTalk != null)
            {
                _trainingRepository.Delete(oldTalk);

                await _trainingRepository.SaveChangesAsync();
            }
        }
    }
}
