import React, { useState, useEffect } from 'react';
import moment from "moment/moment";
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  Button,
  FormControl,
  FormLabel,
  Input,
  useDisclosure
} from '@chakra-ui/react';

const ProjectEditModal = ({ isOpen, onClose, project, onSave }) => {
  const [editedProject, setEditedProject] = useState({ ...project });

  useEffect(() => {
    setEditedProject({ ...project });
  }, [project]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEditedProject({ ...editedProject, [name]: value });
  };

  const handleSave = () => {
    onSave(editedProject);
    onClose();
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Edit Project</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <FormControl mb={4}>
            <FormLabel>Name</FormLabel>
            <Input name="name" value={editedProject.name} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Customer Company Name</FormLabel>
            <Input name="customerCompanyName" value={editedProject.customerCompanyName} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Contractor Company Name</FormLabel>
            <Input name="contractorCompanyName" value={editedProject.contractorCompanyName} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Start Date</FormLabel>
            <Input name="startDateTime" type="date" value={editedProject.startDateTime} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>End Date</FormLabel>
            <Input name="endDateTime" type="date" value={editedProject.endDateTime} onChange={handleChange} />
          </FormControl>
          <FormControl mb={4}>
            <FormLabel>Priority</FormLabel>
            <Input name="priority" value={editedProject.priority} onChange={handleChange} />
          </FormControl>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="blue" mr={3} onClick={handleSave}>
            Save
          </Button>
          <Button variant="ghost" onClick={onClose}>Cancel</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default ProjectEditModal;
