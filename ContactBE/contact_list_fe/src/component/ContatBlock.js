import React from 'react';
import axios from 'axios';

const ContatBlock = ({name,number,setIsModalOpen,setModifyData,Id,fetchContacts}) => {
    const API_URL = "https://localhost:7240/v1/Contact";

    const Modification = async () =>{
        let contact = await axios.get(`${API_URL}/${Id}`);
        setModifyData(contact.data);
        setIsModalOpen(true);
    }

    const DeleteContact = async () =>{
       try {
        await axios.delete(`${API_URL}/${Id}`);
        fetchContacts();
        } catch (error) {
          console.error('Error deleting contact:', error);
        }
    }
    return (
        <div className='ContactBody'>
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
    )
}

export default ContatBlock