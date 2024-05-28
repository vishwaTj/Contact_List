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
  const API_URL = "https://localhost:7240/v1/contacts";
  
  // States for animation
  const [deleteroll, setDeleteRoll] = useState(false);
  const [updateRoll, setUpdateRoll] = useState(false);

  // Modify a contact function ///////////////////////
  const Modification = async () => {
    try {
      setUpdateRoll(true);
      const contact = await axios.get(`${API_URL}/${Id}`);
      setModifyData(contact.data);
      setIsModalOpen(true);
      setTimeout(() => {
        setUpdateRoll(false);
      }, 2000); 
    } catch (error) {
      console.error('Error fetching contact:', error);
    }
  }

  // Delete a contact function ////////////////////
  const DeleteContact = async () => {
    try {
      await axios.delete(`${API_URL}/${Id}`);
      setDeleteRoll(true);
      setTimeout(() => {
        fetchContacts();
        setDeleteRoll(false);
      }, 2000); 
    } catch (error) {
      console.error('Error deleting contact:', error);
    }
  }

  return (
    <div className={classNames('ContactBody', { 'rollOff': deleteroll, 'rotateHorizontal': updateRoll })}>
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
