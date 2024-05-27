import { useEffect, useState,useCallback } from 'react';
import './App.css';
import AddModifyModal from './component/AddModifyModal';
import ContatBlock from './component/ContatBlock';
import axios from 'axios';

function App() {
  const [contactList, setContactList] = useState([]);
  const [modifyData,setModifyData] = useState(null);
  
  const API_URL = "https://localhost:7240/v1/Contact";

  const getContacts = async () => {
    try {
      const response = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      console.error('Error fetching contacts', error);
      throw error;
    }
  };

  const fetchContacts = useCallback(async () => {
      try {
        const data = await getContacts();
        setContactList(data);
      } catch (error) {
        console.error('Error fetching contacts', error);
      }
    },[contactList]);

  useEffect(() => {
    fetchContacts();
  },[]);





  
  const [isModalOpen,setIsModalOpen] = useState(false);

  const toggleModal = ()=>{
    if(modifyData!=null){
      setModifyData(null);
    }
    setIsModalOpen(!isModalOpen);
  }

  const addContact = async (contact) => {
    const { name, number } = contact;
  const newContact = { name, number };

    try {
      if (modifyData) {
        const modifyContact = { id: modifyData.id, name, number };
        await axios.put(API_URL, modifyContact);
      } else {
        await axios.post(API_URL, newContact);
      }
      fetchContacts();
    } catch (error) {
      console.error('Error saving contact', error);

    }
  };

  return (
    <div className="App">
        <div className='Body'>
           <div className='header'>
             <h2>Contact List Application</h2>
             <div className='AddNew'>
              <button onClick={toggleModal}>Add new</button>
             </div>
           </div>
             {contactList.map(contact => (
              <ContatBlock
                  key = {contact.id}
                  name = {contact.name}
                  number = {contact.number}
                  setModifyData = {setModifyData}
                  Id = {contact.id}
                  setIsModalOpen = {setIsModalOpen}
                  fetchContacts = {fetchContacts}
               />
             ))}
        </div>
        {isModalOpen &&
          (<AddModifyModal
          onClose={() => setIsModalOpen(false)}
          modifyData = {modifyData}
          onSave={(newContact) => {
            addContact(newContact);
            toggleModal();
          }}
        />)

        }
    </div>
  );
}

export default App;
