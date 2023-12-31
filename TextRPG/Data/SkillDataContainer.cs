﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class SkillDataContainer : DataReader
    {
        private Dictionary<string, Skill> _skills;

        public SkillDataContainer()
        {
            _skills = new Dictionary<string, Skill>();
        }


        public override void Process(string[] data)
        {
            Skill skill = new Skill();
            skill.NameID = data[0];
            skill.Mp = int.Parse(data[1]);
            skill.CoolTime = int.Parse(data[2]);
            skill.DamegePer = float.Parse(data[3]) / 100f;

            _skills.Add(data[0], skill);
        }

        public Skill CreateSkill(string name)
        {
            return new Skill(_skills[name]);
        }
    }
}
