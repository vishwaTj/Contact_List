import { useEffect, useState,ChangeEvent, useCallback } from 'react';
import './App.css';
import AddModifyModal from './component/AddModifyModal';
import ContactBlock from './component/ContactBlock';
import axios from 'axios';
import classNames from 'classnames';

interface Contact {
  id?: string;
  name: string;
  number: string;
}

function App() {
  const [contactList, setContactList] = useState<Contact[]>([]);
  const [FilteredContacts,setFilteredContacts] = useState<Contact[]>([]);
  const [modifyData, setModifyData] = useState<Contact | null>(null);
  const [searchString,setSearchString] = useState<string>('');

  const API_URL = "https://localhost:7240/v1/contacts";

  // Fetch contacts /////////////////////
  const getContacts = async (): Promise<Contact[]> => {
    try {
      const response = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      console.error('Error fetching contacts', error);
      throw error;
    }
  };

  // Add Default contacts
  const AddDefaultContacts = async (): Promise<void> =>{
    try{
      await axios.get(`${API_URL}/default`);
      fetchContacts();
    }catch(error){
      console.error('Error fetching contacts', error);
      throw error;
    }
  }

  // Function to Search Contacts /////////////
  const FilterContacts = (e: ChangeEvent<HTMLInputElement>) =>{
    const searchValue = e.target.value;
    setSearchString(searchValue);

    if (searchValue === '') {
      setFilteredContacts([]);
    } else {
      const filterContacts = contactList.filter(x =>
        x.name.toLowerCase().includes(searchValue.toLowerCase())
      );
      setFilteredContacts([...filterContacts]);
    }
    console.log(FilterContacts);
  }

  // function to fetch Contacts //////////////
  const fetchContacts = useCallback(async () => {
    try {
      const data = await getContacts();
      setContactList(data);
    } catch (error) {
      console.error('Error fetching contacts', error);
    }
  }, []);

  useEffect(() => {
    fetchContacts();
  }, [fetchContacts]);

  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  // Input Modal toggle //////////////
  const toggleModal = () => {
    if (modifyData != null) {
      setModifyData(null);
    }
    setIsModalOpen(!isModalOpen);
  };

  // Function to add or Modify Contact /////////////////
  const addContact = async (contact: Contact) => {
    const { name, number } = contact;
    const newContact = { name, number };

    try {
      if (modifyData) {
        const modifyContact = { id: modifyData.id, name, number };
        await axios.put(API_URL, modifyContact);
      } else {
        const response = await axios.post(API_URL, newContact);
        console.log(response);
      }
      fetchContacts();
    } catch (error) {
      console.error('Error saving contact', error);
    }
  };

  return (
    <div className="App">
     {/* *********  Main Body Contct list ******** */}
     <div className="search-container">
      <input className="Search-bar" type="text" onChange={FilterContacts} placeholder="Search contacts..." />
       <i className={"fa-solid fa-magnifying-glass" + (searchString.length !== 0 ? " invisible" : " search-icon")}></i>

      </div>
      <div className='Body'>
        <div className='header'>
          <h2>Contact List Application</h2>
          <div className='main-buttons'>
            <div className='AddNew'>
              <button className="button-toggle" onClick={toggleModal}>Add new</button>
            </div>
            <div className='AddDefualt'>
              <button className="button-toggle" onClick={AddDefaultContacts}>Add Default contacts</button>
            </div>
          </div>
        </div>
        {FilteredContacts.length === 0 ? (contactList.map(contact => (
          <ContactBlock
            key={contact.id}
            name={contact.name}
            number={contact.number}
            setModifyData={setModifyData}
            Id={contact.id!}
            setIsModalOpen={setIsModalOpen}
            fetchContacts={fetchContacts}
          />
        ))) : (FilteredContacts.map(contact => (
          <ContactBlock
            key={contact.id}
            name={contact.name}
            number={contact.number}
            setModifyData={setModifyData}
            Id={contact.id!}
            setIsModalOpen={setIsModalOpen}
            fetchContacts={fetchContacts}
          />)))
        }
      </div>

      {/* *********  Modal section for input and modification *********** */}
      {isModalOpen && (
        <AddModifyModal
          onClose={() => setIsModalOpen(false)}
          modifyData={modifyData}
          onSave={(newContact: Contact) => {
            addContact(newContact);
            toggleModal();
          }}
        />
      )}
    </div>
  );
}

export default App;
