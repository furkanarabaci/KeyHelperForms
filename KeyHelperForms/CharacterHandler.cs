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
            if(FindCharacter(characterProcess.Id) == null)
            {
                characterList.Add(new Character(characterProcess));
            }
            else
            {
                //Means the character is already on the list, we don't want any duplicates.
                //TODO : Empty for now, you may raise some error.
            }
        }
        public void RemoveCharacter(Process characterProcess)
        {
            Character currentCharacterObject = FindCharacter(characterProcess.Id);
            if ( currentCharacterObject == null)
            {
                //Means we are trying to delete an existing object, don't do anything.
            }
            else
            {
                characterList.Remove(currentCharacterObject);
            }
            
        }
        public void StartCharacterPress(int index)
        {
            characterList[index].StartPressing();
        }
        public void StopCharacterPress(int index)
        {
            characterList[index].StopPressing();
        }
        public Character FindCharacter(int processId) //MAY RETURN NULL, BE WARY.
        {
            foreach(Character currentChar in characterList)
            {
                if(currentChar.ProcessId == processId)
                {
                    return currentChar;
                }
            }
            return null;
        }
        public void ResetCharacters()
        {
            characterList.Clear();
        }
    }
}
