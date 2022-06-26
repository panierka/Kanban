using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    internal class SubtasksManager
    {
        private User? user;

        public void SetUser(User? user)
        {
            this.user = user;
        }

        public Subtask CreateSubtask(int jobId)
        {
            Subtask subtask = new("Nowe podzadanie", jobId);
            SubtaskRepository.InsertSubtask(subtask, out _);
            return subtask;
        }

        internal void DeleteSubtask(Subtask subtask)
        {
            SubtaskRepository.RemoveSubtask(subtask, out _);
        }
    }
}
