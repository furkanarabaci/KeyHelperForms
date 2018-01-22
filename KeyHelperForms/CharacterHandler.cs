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
        public List<Character> Characters { get; private set; }
        public CharacterHandler()
        {
            Characters = new List<Character>();
        }
        public void AddCharacter(Process characterProcess)
        {
            if (FindCharacter(characterProcess.Id) == null)
            {
                Characters.Add(new Character(characterProcess));
            }
            else
            {
                //Means the character is already on the list, we don't want any duplicates.
            }
        }
        public void RemoveCharacter(int processId)
        {
            Character currentCharacterObject = FindCharacter(processId); //It will return null at non existant pid.
            if (currentCharacterObject == null)
            {
                //Means we are trying to delete a non-existant object, don't do anything.
            }
            else
            {
                Characters.Remove(currentCharacterObject);
            }

        }
        public void RemoveCharacter(Character objectToRemove)
        {
            try
            {
                Characters.Remove(objectToRemove);
            }
            catch (ArgumentNullException)
            {
                //Maybe raise an error, no need for now. Just don't remove null object.
            }

        }
        public bool RenewAndCheckForChange(List<Process> newProcesses)
        {
            //TODO : I didn't like the name of the method, you could change it.
            bool didSomethingChange = false;
            //Check the status of every process and remove dead ones, also add the new ones.
            foreach(Character currentCharacter in Characters) //There may be some dead processes. We must find them... AND DESTROY THEM !
            {
                if (currentCharacter.ClientProcess.HasExited)
                {
                    RemoveCharacter(currentCharacter); //Delete exited process. Garbage collector will handle the rest.
                    didSomethingChange = true;
                }
                if(Characters.Count == 0)
                {
                    break; //Probably better than dealing with exception.
                }
            }
            foreach(Process thisNewProcess in newProcesses) //After destructon, construction always comes.
            {
                AddCharacter(thisNewProcess); //Remember that this method handles duplicate process, do don't worry.
                didSomethingChange = true;
            }
            return didSomethingChange;
        }
        public void StartCharacterPress(int index)
        {
            Characters[index].StartPressing();
        }
        public void StopCharacterPress(int index)
        {
            Characters[index].StopPressing();
        }
        public Character FindCharacter(int processId) //MAY RETURN NULL, BE WARY.
        {
            foreach (Character currentChar in Characters)
            {
                if (currentChar.ClientProcess.Id == processId)
                {
                    return currentChar;
                }
            }
            return null;
        }
        public void ResetCharacters()
        {
            Characters.Clear();
        }
        public int GetCharacterCount()
        {
            return Characters.Count;
        }
    }
}
