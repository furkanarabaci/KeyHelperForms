using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyHelperForms
{
    class CharacterHandler
    {
        List<Character> characterList;
        public CharacterHandler()
        {
            characterList = new List<Character>();
        }
        public void AddCharacter(Process characterProcess)
        {
            characterList.Add(new Character(characterProcess));
        }
        public void RemoveCharacter(Process characterProcess)
        {
            characterList.Remove(FindProcess(characterProcess.Id));
        }
        public void StartCharacterPress(int index)
        {
            characterList[index].StartPressing();
        }
        public void StopCharacterPress(int index)
        {
            characterList[index].StopPressing();
        }
        protected Character FindProcess(int processId)
        {
            foreach(Character currentChar in characterList)
            {
                if(currentChar.ProcessId == processId)
                {
                    return currentChar;
                }
            }
            return null; //It usually wont reach here, just in case.
        }
        public void ResetCharacters()
        {
            characterList.Clear();
        }
    }
}
