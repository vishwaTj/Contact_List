import React, { useState, ChangeEvent, FormEvent } from 'react';

interface Contact {
  name: string;
  number: string;
}

interface AddModifyModalProps {
  onClose: () => void;
  onSave: (contact: Contact) => void;
  modifyData?: Contact | null;
}

const AddModifyModal: React.FC<AddModifyModalProps> = ({ onClose, onSave, modifyData }) => {
  
  // states to set Name Number and Error input 
  const [name, setName] = useState(modifyData?.name || '');
  const [number, setNumber] = useState(modifyData?.number || '');
  const [errors, setErrors] = useState<{ name?: string; number?: string }>({});

  // Validate input if it matches the required conditions ////////////////////
  const validateInput = (name: string, value: string) => {
    let error = '';
    if (name === 'name' && value.length > 12) {
      error = ' Name must be less than 12 characters';
    } else if (name === 'number' && (!/^\d{10}$/.test(value))) {
      error = ' Number must be 10 digits';
    }
    setErrors(prevErrors => ({ ...prevErrors, [name]: error }));
  };


  // Apply the Changes made in the input /////////////////
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (name === 'name') {
      setName(value);
    } else if (name === 'number') {
      setNumber(value);
    }
    validateInput(name, value);
  };

  // Save the Modified input ////////////////////////
  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    if (!errors.name && !errors.number) {
      const newContact: Contact = {
        name,
        number
      };
      onSave(newContact);
    }
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h2>Add/Modify Contact</h2>
        <form onSubmit={handleSubmit}>
          <div className='name-input'>
            <label>Name</label>
            <input
              type="text"
              name="name"
              value={name}
              onChange={handleChange}
              required
            />
            {errors.name && <span className="error">{errors.name}</span>}
          </div>
          <div className='number-input'>
            <label>Number</label>
            <input
              type="text"
              name="number"
              value={number}
              onChange={handleChange}
              required
            />
            {errors.number && <span className="error">{errors.number}</span>}
          </div>
          <button className='submit-btn button-toggle' type="submit">Save</button>
          <button className='close-btn button-toggle' type="button" onClick={onClose}>Close</button>
        </form>
      </div>
    </div>
  );
};

export default AddModifyModal;
