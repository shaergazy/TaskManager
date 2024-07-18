import { useState } from 'react';
import { Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton, Input, FormControl, FormLabel } from '@chakra-ui/react';
import moment from "moment/moment";

const EditEmployeeModal = ({ isOpen, onClose, employee, onSave }) => {
  const [editedEmployee, setEditedEmployee] = useState({...employee});

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditedEmployee(prevState => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleUpdate = () => {
    onSave(editedEmployee);
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Edit Employee</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <FormControl>
            <FormLabel>First Name</FormLabel>
            <Input name="firstName" value={editedEmployee.firstName} onChange={handleChange} />
          </FormControl>
          <FormControl mt={4}>
            <FormLabel>Last Name</FormLabel>
            <Input name="lastName" value={editedEmployee.lastName} onChange={handleChange} />
          </FormControl>
          <FormControl mt={4}>
            <FormLabel>Email</FormLabel>
            <Input name="email" value={editedEmployee.email} onChange={handleChange} />
          </FormControl>
          <FormControl mt={4}>
            <FormLabel>Phone Number</FormLabel>
            <Input name="phoneNumber" value={editedEmployee.phoneNumber} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Birth Date</FormLabel>
            <Input name="birthDate" type="date" value={editedEmployee.birthDate} onChange={handleChange} />
          </FormControl>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="blue" mr={3} onClick={handleUpdate}>Save</Button>
          <Button onClick={onClose}>Cancel</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default EditEmployeeModal;
