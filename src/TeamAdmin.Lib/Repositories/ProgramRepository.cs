using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Lib.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private List<Program> programs;

        public ProgramRepository()
        {
            InitializePrograms();
        }

        private void InitializePrograms()
        {
            programs = new List<Program>();
            programs.Add(new Program {
                    ProgramId = 1, Title = "Pre Academy Development Program",
                    Body = @"<p>The Pre academy development program (PADP) is structured towards players who want to work in an Academy environment, and constantly work towards bringing up their overall skills level of soccer. This program contains both full time athletes whose goal is to progress into one of our competitive academy program (CAP) and also athletes who want to work under Mapola FC & Academy development systems but is committed to other organizations. Players in this structure will train 2 times a week.</p>
                             <p>Any (PADP) player is  eligible and encouraged to work their way up in to our full time competitive program, but first either must  demonstrate the commitment, talent and capacity to handle the demands of the (CAP) The program will commence full time at the beginning of April and will be separate from the (CAP) training  and game schedule. A more intimate ratio of training will be kept at 1 coach per 12 kids grouped in settings based on criteria set forth by the Academy Director. (PADP) athletes will play a minimum of one game every 15 - 20 days in competitive setups unless specifically declined upon registration of the Athlete by parent.</p>  
                             <p>(PADP) athletes will be measured against (CAP) once per month with no regard to age or size All (PADP) athletes will be given clear feedback from Mapola FC regarding their progress within the (PADP). Reports will be sent in via online resources to both player and parent, with detailed information on the athlete and recommendations moving forward. This information will be set at the discretion of the Director of Players Development  of  Mapola FC with input from Coaches. Any third party organization club team or small rep team setting looking for extra training will immediately  default into our (PADP) setup  – no athletes that fall under this category will be eligible to train or mix in with  any (CAP) unless specified by the Academy Director.</p>
                             <p>Training will be held twice a week for 4 hours. Focus Areas:
                                <ul><li>Speed training</li><li>Skill and Dribbling</li><li>Ball Movement and Quick Touches</li><li>Ball Control and Turning with the Ball</li><li>Passing and Receiving</li><li>Acceleration and Reaction</li><li>Soothing and Shielding</li><li>Speed training</li><li>Positioning</li></ul>
                            </p>"
                });

            programs.Add(new Program {
                    ProgramId = 2, Title = "Academy Development Program U6-U8",
                    Body = @"<p>Mapola FC Academy offers a Micro Developmental Soccer Program for 6-8 year old children. Our club provides a fun and safe environment to learn to play soccer through age appropriate activities and small-sided soccer games. The Micro developmental Soccer is a program that includes age appropriate activities involving games or activities where all the players are involved and the activity is appropriate for the player’s age group and development. Children need to be successful to learn. Asking a 6 year old or younger player to play a position or formation is setting them up for failure. They do not have the technical or cognitive ability to play a position successfully.</p>
                            <p>Winter/Spring  Micro  Soccer  will  take  place  Sunday  Morning  or  Friday  night,  Dates  and  times  are  determined  and  are  set  by  the  program  director. Summer Micro Soccer will take place either on Monday/Wednesday or Tuesday/Thursday. Times are either 6:00pm-7:00pm or 7:00pm-8:00pm. Dates and times are determined prior to the start of the season and are set by the program director.  The season for summer micro-soccer is the same as the season for the summer Elite program.</p>
                            "});

            programs.Add(new Program
                {
                    ProgramId = 3,
                    Title = "Elite Program U10-14",
                    Body = @"<p>Our Elite academy program offers 3 Practices a week and 1 - 2 game a week, and run an all year programs. Practice time and day may depend on the athlete’s age group. We also provide 5v5 futsal game format activity 3 times a week in a competitive environment during the winter. We provide Strength and Conditioning training once every 2 weeks, with one nutritional session once a month, and Technical skills training one a week.
Our team age 9 – 14 plays in OASL that mea ns we travel the GTA for most of our games with some home games. Our program is dedicated to excellence therefore we take proud in our teaching, as well as prioritizing our participants need. Our elite players train hard in a fun environment and play right.  Please refer to our events calendar for training information’s. Our elites players play in OASL League April - November and participate yearly in 5-6 domestic tournament and 1-2 major tournaments in the US or Europe, age dependent.</p>"
            });

            programs.Add(new Program
            {
                ProgramId = 4,
                Title = "Curriculum",
                Body = @"<h3>Academy Culture</h3>
                         <p><ul>
<li>Promote enjoyment</li>
<li>Provide a long term learning environment to allow individuals to maximize their football talent and potential</li>
<li>Develop lifelong individual personalities-attitude, discipline, concentration, team work, confidence, leadership, respect, motivation</li>
<li>Develop individual player technical, tactical, physical, psychological and social qualities</li>
<li>A training environment and playing style to allow the players to problem solve, express themselves and make decisions</li>
<li>Seek to improve everyday</li>
<li>Trust each other</li>
</ul></p> 
<h3>Coaching Methodology</h3>  
<p>We believe that effective coaching is critical in the development of our young players. Our football culture is clearly defined in which we must create the followingenvironment to: </p>
<p>
<ul>
<li>Building play from the back and through all units of the team </li>
<li>Understand the needs of the individual players and provide a support mechanism to cater for those needs</li>
<li>Deliver age related practices that support learning </li>
<li>Develop the person to develop the player </li>
<li>Defending with organization, discipline and responsibility </li>
<li>Attacking with imagination, invention and expression </li>
<li>Develop the transference of skills and knowledge from the training environment into the games programme </li>
<li>Ensure everything we do in the coaching and development of our players is with a clear method and purpose </li>
<li>Teach players to play with freedom, encouraging experimentation in the younger players and expression throughout all ages </li>
<li>Encouraging the importance of hard wok, application and effort as part of the long-term player pathway</li>
</ul>
</p>
<h3>Style of Play</h3> 
<p>Our Academy style of play is built on an attacking and progressive possession based
approach. We teach this through building confidence, awareness and the ability to
retain possession in all areas of field. Players are encouraged to express themselves
against opponents in game situations. </p>
<p>Out of possession we adopt a collective and assertive defensive approach. This is
built around technically effective individual defending (1v1 situations) and a tactical
competence to defend collectively with teammates.
</p>
<p>Our style of play is developed over a period of time through a program managed by
staff and coaches who place individual player performance and development as the
priority within the coaching curriculum’s.</p>
<p><a target='_blank' href='/forms/Curriculum.pdf'>Click here to download the full curriculum.</a>
"
            });
        }

        public Program GetProgram(int programId)
        {
            return programs.FirstOrDefault(programs => programs.ProgramId == programId);
        }

        public IEnumerable<Program> GetPrograms()
        {
            return programs;
        }
    }
}
