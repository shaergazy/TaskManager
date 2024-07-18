import { useState } from 'react';
import { Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton, Input, FormControl, FormLabel } from '@chakra-ui/react';

const CreateEmployeeModal = ({ isOpen, onClose, onCreate }) => {
  const [employee, setEmployee] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEmployee(prevState => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleAdd = () => {
    onCreate(employee);
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add Employee</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <FormControl>
            <FormLabel>First Name</FormLabel>
            <Input name="firstName" value={employee.firstName} onChange={handleChange} />
          </FormControl>
          <FormControl mt={4}>
            <FormLabel>Last Name</FormLabel>
            <Input name="lastName" value={employee.lastName} onChange={handleChange} />
          </FormControl>
          <FormControl mt={4}>
            <FormLabel>Email</FormLabel>
            <Input name="email" value={employee.email} onChange={handleChange} />
          </FormControl>
          <FormControl mt={4}>
            <FormLabel>Phone Number</FormLabel>
            <Input name="phoneNumber" value={employee.phoneNumber} onChange={handleChange} />
          </FormControl>
          <FormControl>
          <Input
                        type="date"
                        placeholder='Birth Date'
                        name='birthDate'
                        value={employee.birthDate}
                        onChange={handleChange}
                    />
          </FormControl>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="blue" mr={3} onClick={handleAdd}>Add</Button>
          <Button onClick={onClose}>Cancel</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default CreateEmployeeModal;
