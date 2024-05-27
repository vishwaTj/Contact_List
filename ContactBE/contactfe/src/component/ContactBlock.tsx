import React, { useState } from 'react';
import axios from 'axios';
import classNames from 'classnames';

interface ContactBlockProps {
  name: string;
  number: string;
  setIsModalOpen: (isOpen: boolean) => void;
  setModifyData: (data: any) => void;
  Id: string;
  fetchContacts: () => void;
}

const ContactBlock: React.FC<ContactBlockProps> = ({ name, number, setIsModalOpen, setModifyData, Id, fetchContacts }) => {
  const API_URL = "https://localhost:7240/v1/Contact";
  const [deleteroll, setDeleteRoll] = useState(false);

  const Modification = async () => {
    try {
      const contact = await axios.get(`${API_URL}/${Id}`);
      setModifyData(contact.data);
      setIsModalOpen(true);
    } catch (error) {
      console.error('Error fetching contact:', error);
    }
  }

  const DeleteContact = async () => {
    try {
      await axios.delete(`${API_URL}/${Id}`);
      setDeleteRoll(true);
      setTimeout(() => {
        fetchContacts();
        setDeleteRoll(false); // Reset the state if you want to reuse the component
      }, 2000); // Duration should match the CSS animation duration
    } catch (error) {
      console.error('Error deleting contact:', error);
    }
  }

  return (
    <div className={classNames('ContactBody', { 'rollOff': deleteroll })}>
      <div className='ContactName'>
        {name}
      </div>
      <div className='ContactNumber'>
        <div>
          {number}
        </div>
        <div className='icon-btn' onClick={Modification}>
          <i className="fa-solid fa-pen-to-square"></i>
        </div>
        <div className='icon-btn' onClick={DeleteContact}>
          <i className="fa-solid fa-trash"></i>
        </div>
      </div>
    </div>
  );
}

export default ContactBlock;
