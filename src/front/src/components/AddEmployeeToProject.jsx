import React, { useState } from 'react';
import { Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton, Select } from "@chakra-ui/react";

const AddEmployeeToProjectModal = ({ employees, isOpen, onClose, onAddEmployee, projectId }) => {
  const [selectedEmployeeId, setSelectedEmployeeId] = useState('');

  const handleAddEmployee = () => {
    onAddEmployee(projectId, selectedEmployeeId);
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add Employee to Project</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Select placeholder="Select employee" value={selectedEmployeeId} onChange={(e) => setSelectedEmployeeId(e.target.value)}>
            {employees?.map(employee => (
              <option key={employee.id} value={employee.id}>{employee.email}</option>
            ))}
          </Select>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="blue" mr={3} onClick={handleAddEmployee}>Add</Button>
          <Button onClick={onClose}>Cancel</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default AddEmployeeToProjectModal;
